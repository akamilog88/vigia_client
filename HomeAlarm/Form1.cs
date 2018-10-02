using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using System.Speech.Synthesis;
using System.ServiceModel;
using SenApi.ServicesContract.PubSubContract;
using SenApi.Services.PubSubServices;

using HomeAlarm.DAL;
using HomeAlarm.Controllers;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SenApi.BaseSensorsContract;
using SenApi.ServicesContract.ParallelPort;


namespace HomeAlarm
{
   
    public partial class Form1 : Form
    {
        ServiceHost host;
        List<SensorGroupCtrl> ls;
        Thread reciverThread;
        EventReciverService reciver;
       
        bool eventDetected;

        bool detecting;

        bool speechAble = true;

        bool doCall = false;
        Uri doCallpportAddress;

        int countCalls=0;
        System.Timers.Timer calltimer;
        System.Timers.Timer timer;
        System.Threading.Timer netCheker;

        int netChekInterval;
        SpeechSynthesizer speaker;
        List<String> messages;

        object syncro;
        object reciv_lock;

        XDocument d;
        AlarmContext dbcontext;        
        
        public Form1()
        {
            InitDB();
            syncro = new object();
            reciv_lock = new object();
            detecting=false;

            ls = new List<SensorGroupCtrl>();          
            eventDetected = false;
            messages = new List<String>();
            try
            {
                speechAble = Convert.ToBoolean(ConfigurationManager.AppSettings["speak"]);
            }
            catch (Exception e) {
                speechAble = false;
            }

           
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = false;

            calltimer = new System.Timers.Timer();
            calltimer.Elapsed += calltimer_Elapsed;
            calltimer.Interval = 30000;
            calltimer.Elapsed += timer_Elapsed;
            calltimer.Enabled = false;

            InitializeComponent();
            this.GB_filterPanel.Hide();
            this.LB_status.Text = "";
            BuildDevicesControls();
            speaker = new SpeechSynthesizer();

            this.FormClosing += Form1_FormClosing;
        }

        void calltimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CAllProcedure();
        }

        void NetChekingFunc(object o) { 
            foreach(var sg in ls){
                
                if (DateTime.Now - sg.lastContact > new TimeSpan(0, netChekInterval/60000, 0)) {
                    bool registerOnly = false;
                    if (!sg.IsConnected)
                        registerOnly = true; 
                    Action update = () =>
                    {
                    sg.Sensors.ForEach(s => s.Fired = true); 
                    TAB_alarmGroup.SelectedTab = TAB_alarmGroup.TabPages[sg.Code];
                    RegisterEvent(DateTime.Now,"Pérdida de Conexión", sg.Code, sg, "",registerOnly);
                    };
                    this.Invoke(update);
                    sg.IsConnected = false;
                }
            }
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {          
            //d.Save(ConfigurationSettings.AppSettings["XMLConfigName"]);
        }
        void InitDB() 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AlarmContext>());
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            dbcontext = new AlarmContext();
            if (dbcontext.EventTypes.Count() == 0)
            {
                dbcontext.EventTypes.Add(new EventType { EventName = EventType.EventType_Enabled });
                dbcontext.EventTypes.Add(new EventType { EventName = EventType.EventType_Disabled });
                dbcontext.EventTypes.Add(new EventType { EventName = EventType.EventType_Detection });
                dbcontext.SaveChanges();
            }             
        }
      
        void OnRecive(string event_name,Uri sender,EventData[] data) {
       
            lock (reciv_lock)
            {
                SensorUserCtrl sens=null;
                SensorGroupCtrl g = ls.SingleOrDefault(sg => sender.ToString().Contains(sg.Code));
                Action a ;
                if (g != null)
                    g.lastContact = DateTime.Now;
                else
                    return;

                switch(event_name){                  
                case "DTMF":                 
                    sens = g.Sensors.SingleOrDefault(sns => sns.Code == data[1].Data_Val.ToString() && sns.DeviceType == "DTMF");

                    if (sens != null)
                    {
                        if (!sens.Fired && sens.Active )
                        {
                            RegisterEvent((DateTime)data[0].Data_Val,sens.Message, sens.Code, g, "DTMF", false);
                            a = () => sens.Fired = true;
                            this.Invoke(a);
                        }
                        else
                        {                            
                            RegisterEvent((DateTime)data[0].Data_Val,sens.Message, sens.Code, g, "DTMF", true);
                        }
                    }
                    
                break;
                case "PSC":                        
                    List<SensorUserCtrl> l = g.Sensors.Where(sns => sns.DeviceType == "PSC").ToList();
                    foreach (var s in l)
                    {
                        sens = s;
                        string[] values = s.Code.Split(';');
                        int port = Math.Abs(888 - Convert.ToInt32(values[0].Substring("port:".Length, values[0].Length - "port:".Length)));
                        int value = Convert.ToInt16(values[1].Substring("mask:".Length, values[1].Length - "mask:".Length));
                        int rest = Convert.ToInt16(values[2].Substring("rest:".Length, values[2].Length - "rest:".Length));
                        int incomingVal=(Convert.ToInt16(data[port+1].Data_Val));
                        if (((incomingVal & value) / value) != rest)
                        {
                            if (!sens.Fired && sens.Active)
                            { 
                                a = () => sens.Fired = true;
                                this.Invoke(a);
                                RegisterEvent((DateTime)data[0].Data_Val,sens.Message, sens.Code, g, "PSC", false);
                            }
                            else {
                                RegisterEvent((DateTime)data[0].Data_Val,sens.Message, sens.Code, g, "PSC", true);                               
                            }
                        }else{
                            if (sens.Fired && sens.Active )
                            { 
                                a = () => sens.Fired = false;                               
                                this.Invoke(a);
                                RegisterEvent((DateTime)data[0].Data_Val, "Sensor "+sens.Name +" normalizado", sens.Code, g, "PSC", false);
                            }                            
                        }                        
                    }
                break;             
                case "ENDOR":
                a = () => g.Sensors.ForEach(s => s.Fired = true);
                this.Invoke(a);
                RegisterEvent((DateTime)data[0].Data_Val, "Detector Comprometido", sender.ToString(), g, "",false);
                break;            
                }
            }
        }

        private void RegisterEvent(DateTime sendTime,String Message, String Code, SensorGroupCtrl g, String Type,bool registerOnly)
        {
            EventsController c;
            lock (dbcontext)
            {
                try
                {
                    c = new EventsController(dbcontext);
                    c.RegisterNewEvent(new Event { Send_Date= sendTime, Reciv_Date = DateTime.Now, Messaje = Message, Code = Code, DeviceType = Type, SensorsInfo = d.Element("Devices").Elements("DeviceGroup").Single(x=>x.Attribute("code").Value ==g.Code).ToString(SaveOptions.DisableFormatting), SensorsGroup = new SensorsGroup { Name = g.CodeName, Address = g.Code, DescriptiveName = g.DesciptiveName } }, EventType.EventType_Detection);
                }
                catch (Exception e) {
                    string s = e.Message;
                }
            }
            if (registerOnly)
            {
                return;
            }
            Action update = () =>
            {
                TAB_alarmGroup.SelectedTab = TAB_alarmGroup.TabPages[g.Code];
                TA_logArea.AppendText(g.DesciptiveName + ": " + DateTime.Now.ToShortTimeString() + " -->" + Message +".Hora de Envío: "+ sendTime.ToLongTimeString()+ "\n");             
            };
            this.Invoke(update);
            messages.Add(Message + ". Instancia, " + g.DesciptiveName);
            if(speechAble)
            try
            {
                speaker.SpeakAsyncCancelAll();
                speaker.SpeakAsync(Message + ". Instancia, " + g.DesciptiveName);
            }
            catch(Exception) {
                speechAble = false;
            }
         
            if (!eventDetected)
            {
                eventDetected = true;
                if (doCall)
                {
                    CAllProcedure();
                    calltimer.Enabled = true;
                    calltimer.Start();
                }
            }
            timer.Start();
        }

        void CAllProcedure() {
            if (countCalls < 4)
            {
                Action d0 = () =>
                {
                try
                {
                    ChannelFactory<IPortServerContract> factory = new ChannelFactory<IPortServerContract>("PPort", new EndpointAddress(doCallpportAddress));
                    IPortServerContract client = factory.CreateChannel();
                    int readed = client.ReadPortValue(888);
                    client.WritePortValue(888, (readed&254) + 1);
                    Thread.Sleep(new TimeSpan(0, 0, 20));
                    readed = client.ReadPortValue(888);
                    client.WritePortValue(888, readed & 254);
                    factory.Close();
                }
                catch (Exception e)
                {
                    //MessageBox.Show("La aplicación no pudo ejecutar llamada.\n", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                };
                Action d1 = () =>
                {
                try
                {
                    ChannelFactory<IPortServerContract> factory = new ChannelFactory<IPortServerContract>("PPort", new EndpointAddress(doCallpportAddress));
                    IPortServerContract client = factory.CreateChannel();
                    int readed = client.ReadPortValue(888);
                    client.WritePortValue(888, (readed & 253) + 2);
                    Thread.Sleep(new TimeSpan(0, 0, 2));
                    readed = client.ReadPortValue(888);
                    client.WritePortValue(888, readed & 253);
                    factory.Close();
                }
                catch (Exception e)
                {
                    //MessageBox.Show("La aplicación no pudo ejecutar llamada.\n", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                };
                d0.BeginInvoke(null, null);
                Thread.Sleep(200);
                d1.BeginInvoke(null, null);
                countCalls++;
            }
            else
            {
                calltimer.Enabled = false;               
                calltimer.Stop();
            }
         
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (syncro)
            {
                if (speechAble)
                {
                    string long_message = "";
                    messages.ForEach(m => long_message += m + ". ");
                    speaker.SpeakAsync(long_message);
                }
                else {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }
        void BuildDevicesControls() {
            string xmlpath = ConfigurationManager.AppSettings["XMLConfigName"];
            d = XDocument.Load(xmlpath);         
            IEnumerable<XElement> l = d.Element("Devices").Elements("DeviceGroup");
            foreach(XElement dg in l){
                string pagekey = dg.Attribute("code").Value;
                string rname = dg.Attribute("name").Value;
                string dname = dg.Attribute("desciption").Value;
                bool local = Convert.ToBoolean(dg.Attribute("local").Value);
                bool dtmf = Convert.ToBoolean(dg.Attribute("dtmf").Value);
                bool pport = Convert.ToBoolean(dg.Attribute("pport").Value);
                Uri dtmf_add = null;
                Uri pport_add = null;
                try
                {
                    if (local)
                    {
                        dtmf_add = new Uri(dg.Attribute("dtmf_address").Value);
                        pport_add = new Uri(dg.Attribute("pport_address").Value);
                    }
                }catch(Exception){
                    MessageBox.Show("Dirección de red no valida.\nVerifique el fichero de configuracion Devices.xml", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                TAB_alarmGroup.TabPages.Add(pagekey, rname,dname);
                int index = TAB_alarmGroup.TabPages.IndexOfKey(pagekey);
                var sg = new SensorGroupCtrl(pagekey, rname, dname, local, dtmf, pport, pport_add, dtmf_add);
                sg.lastContact = DateTime.Now;
                sg.Dock = DockStyle.Fill;
                sg.AutoScroll = true;
                ls.Add(sg);
                TAB_alarmGroup.TabPages[index].Controls.Add(sg);
                foreach(XElement x in dg.Elements("Device")){
                    var s = new SensorUserCtrl { Name = x.Attribute("name").Value, Active = (bool)x.Attribute("active"), Code = x.Attribute("code").Value, Message = x.Attribute("message").Value, DeviceType = x.Attribute("type").Value };
                    s.onActiveChange += onSensorActiveStateChanged;
                    sg.AddSensor(s);  
                }
            }
        }

        void onSensorActiveStateChanged(object sender,EventArgs args) { 
            SensorUserCtrl sender_ref = (SensorUserCtrl)sender;
            if (!sender_ref.Active && sender_ref.Fired)
            {
                if (speechAble)
                {
                    string full_message = null;
                    try
                    {
                        full_message = messages.Single(m => m.Contains(sender_ref.Message));
                    }
                    catch (Exception)
                    {
                    }
                    if (full_message != null)
                    {
                        try
                        {
                            speaker.SpeakAsyncCancelAll();
                        }
                        catch (Exception) { }
                        timer.Stop();
                        messages.Remove(full_message);
                        timer.Start();
                    }
                }
                else {
                    timer.Stop();
                }
            }
        }
        //Boton comenzar solamente va a levantar el servicio de recepcion
        //desde ese punto siempre estara a la escucha de eventos(detectando)
        private void bt_reset_Click(object sender, EventArgs e)
        {
            IniciarRecepcion();   

        }

        private void Form1_Load(object sender, EventArgs e)
        {               
            try
            {
                doCall = Convert.ToBoolean(ConfigurationManager.AppSettings["doCall"]);
                doCallpportAddress = new Uri(ConfigurationManager.AppSettings["doCallpportAddress"]);
                netChekInterval = Convert.ToInt16(ConfigurationManager.AppSettings["NetChekPeriod"]) * 60 * 1000;
            }
            catch (Exception) {
                doCall = false;
                netChekInterval = 5 * 60 * 1000;
                MessageBox.Show("Ocurrieron errores cargando propiedades de configuración.\nVerifique las llaves (doCall y doCallpportAddress).\n", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ReadSensorsStatus();
        }

        void HostReciverService() {
            try
            {
                reciver = new EventReciverService();
                reciver.EventRecibed = OnRecive;
                host = new ServiceHost(reciver);
                host.Open();
            }
            catch (Exception e )
            { 
                MessageBox.Show("No se pudo hospedar el servicio para la recepcion de eventos.\nDetalle: " +e.Message , "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       

        //Se Ejecuta la rutina de Busqueda en el Historial
        private void button1_Click(object sender, EventArgs e)
        {
            this.GB_filterPanel.Show();

            var fd = new FilterDialog();
            fd.ShowDialog();
            if (fd.Accepted)
            {
                TA_logArea.Text = "";
                var ec = new EventsController(dbcontext);
                var evts = ec.ObtenerEventos(fd.FechInicio, fd.FechFin);
                evts.ForEach(s => TA_logArea.AppendText(s.Reciv_Date + "-->" + s.Messaje + "(" + s.SensorsGroup.Name + ")" + ".Hora de Envío:" + s.Send_Date + "\n"));
            }
        }
        //Boton Filtrar(se filtra por el texto contenido en el textbox de filtro)
        private void button2_Click(object sender, EventArgs e)
        {
            String Text = TA_logArea.Text;
            TA_logArea.Text = "";
            String [] splitter = {"\n"};
            String[] result =Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in result) {
                if (s.Contains(TB_Filter.Text))
                    TA_logArea.AppendText(s + "\n");
            }
        }

        void StartSensors(String SensorType)
        {
            List<SensorGroupCtrl> sgs = ls.Where(g => g.isLocal == true).ToList();
            foreach (SensorGroupCtrl sg in sgs)
            {
                if (sg.hasDTMFS && SensorType == "DTMF")
                {
                    try
                    {
                        ChannelFactory<IBaseSensorsContract> factory = new ChannelFactory<IBaseSensorsContract>("DTMF", new EndpointAddress(sg.dtmf_Address));
                        IBaseSensorsContract client = factory.CreateChannel();
                        if (!client.IsDetecting())
                            client.StartDetect();
                        factory.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
                if (sg.hasPPort && SensorType == "PPort")
                {
                    try
                    {
                        ChannelFactory<IPortServerContract> factory = new ChannelFactory<IPortServerContract>("PPort", new EndpointAddress(sg.pport_Address));
                        IBaseSensorsContract client = factory.CreateChannel();
                        if (!client.IsDetecting())
                            client.StartDetect();
                        factory.Close();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
        void StopSensors(String SensorType)
        {
            List<SensorGroupCtrl> sgs = ls.Where(g => g.isLocal == true).ToList();
            foreach (SensorGroupCtrl sg in sgs)
            {
                if (sg.hasDTMFS && SensorType=="DTMF")
                {
                    try
                    {
                    ChannelFactory<IBaseSensorsContract> factory = new ChannelFactory<IBaseSensorsContract>("DTMF", new EndpointAddress(sg.dtmf_Address));
                    IBaseSensorsContract client = factory.CreateChannel();
                    if (client.IsDetecting())
                        client.StopDetect();
                    factory.Close();
                    }
                    catch (Exception e)
                    {                     
                    }
                }
                if (sg.hasPPort && SensorType == "PPort")
                {
                    try{
                    ChannelFactory<IPortServerContract> factory = new ChannelFactory<IPortServerContract>("PPort", new EndpointAddress(sg.pport_Address));
                    IBaseSensorsContract client = factory.CreateChannel();
                    if (client.IsDetecting())
                        client.StopDetect();
                    factory.Close();
                    }
                    catch (Exception e)
                    {                        
                    }
                }
            }            
        }

        private void TAB_alarmGroup_SelectedIndexChanged(object sender, EventArgs e)
        {            
            String name = TAB_alarmGroup.SelectedTab.Name;
            SensorGroupCtrl sg = ls.Single(g => g.Code == name);
            if (sg.isLocal)
            {
                if (sg.IsDTMFActive)
                    LB_dtmfStatus.Text = "Sensores DTMF: Conectados.";
                else
                    LB_dtmfStatus.Text = "Sensores DTMF: Desconectados.";
                if (sg.IsPPortActive)
                    LB_PPortStatus.Text = "Sensores Puerto Paralelo: Conectados.";
                else
                    LB_PPortStatus.Text = "Sensores Puerto Paralelo: Desconectados.";
            }
            else {
                LB_dtmfStatus.Text = "Sensores DTMF: Remoto.";
                LB_PPortStatus.Text = "Sensores Puerto Paralelo: Remoto.";
            }
        }

        void IniciarRecepcion() {
            //INICIANDO SERVICIO DE RECEPCION
            if (!detecting)
            {
                reciverThread = new Thread(HostReciverService);
                reciverThread.Start();
                this.LB_status.Text = "Detección habilitada!";
                this.bt_reset.Text = "Detener";
                      
                netCheker = new System.Threading.Timer(NetChekingFunc, null, 0, netChekInterval);

                try
                {
                    ChannelFactory<IPubSubEventAPI> factory = new ChannelFactory<IPubSubEventAPI>("IPubSubEventAPI");
                    IPubSubEventAPI client = factory.CreateChannel();
                    var r = new ReciverEndpointInfo { Address = new Uri(ConfigurationManager.AppSettings["service_address"]), Binding = "KAKA" };
                    client.SubscribeForEvent("DTMF", r);
                    client.SubscribeForEvent("PSC", r);
                    client.SubscribeForEvent("NSTAT", r);
                    client.SubscribeForEvent("ENDOR", r);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ocurrieron errores durante la subscripción para la recepcion de eventos.\nVerifique que el servicio de publicación y recepción de eventos esté iniciado.\nAlgunos sensores pueden estar desabilitados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TA_logArea.Text = "";
                detecting = true;
                ReadSensorsStatus();
            }
            else {
                try
                {
                    speaker.SpeakAsyncCancelAll();
                }
                catch (Exception exc)
                {

                }
                ls.ForEach(s => s.Reset());
                timer.Enabled = false;
                host.Close();
                reciver.EventRecibed = null;
                this.bt_reset.Text = "Comenzar";
                this.LB_status.Text = "Detección desabilitada!";
                messages.RemoveAll(p => true);
                TA_logArea.Text = "";
                eventDetected = false;                
                GB_filterPanel.Visible = false;
                countCalls = 0;
                detecting = false;
            }   
            /* CountDownDialog conteo = new CountDownDialog(60);
             conteo.ShowInTaskbar = false;
             conteo.ShowDialog(this);*/           
        }

        void ReadSensorsStatus() {
            List<SensorGroupCtrl> sgs;
            sgs= ls.Where(g => g.isLocal == false).ToList();            
            sgs = ls.Where(g => g.isLocal == true).ToList();
            foreach (SensorGroupCtrl sg in sgs)
            {
                if (sg.hasDTMFS)
                {
                    try
                    {
                        ChannelFactory<IBaseSensorsContract> factory = new ChannelFactory<IBaseSensorsContract>("DTMF", new EndpointAddress(sg.dtmf_Address));
                        IBaseSensorsContract client = factory.CreateChannel();
                        if (!client.IsDetecting())
                        {
                            sg.IsDTMFActive = false;
                            LB_dtmfStatus.Text = "Sensores DTMF: Desconectados.";
                            sg.Sensors.Where(sens => sens.DeviceType == "DTMF").ToList().ForEach(sens => sens.Active = false);
                        }
                        else
                        {
                            sg.Sensors.Where(sens => sens.DeviceType == "DTMF").ToList().ForEach(sens => sens.Active = true);
                            LB_dtmfStatus.Text = "Sensores DTMF: Conectados.";
                            sg.IsDTMFActive = true;
                        }
                        factory.Close();                      
                    }
                    catch (Exception e)
                    {
                        sg.IsDTMFActive = false;
                        LB_dtmfStatus.Text = "Sensores DTMF: Desconectados.";
                        sg.Sensors.Where(sens => sens.DeviceType == "DTMF").ToList().ForEach(sens => sens.Active = false);
                    }
                }
                if (sg.hasPPort)
                {
                    try
                    {
                        ChannelFactory<IPortServerContract> factory = new ChannelFactory<IPortServerContract>("PPort", new EndpointAddress(sg.pport_Address));
                        IBaseSensorsContract client = factory.CreateChannel();
                        if (!client.IsDetecting())
                        {
                            LB_PPortStatus.Text = "Sensores Puerto Paralelo: Desconectados.";
                            sg.IsPPortActive = false;
                            sg.Sensors.Where(sens => sens.DeviceType == "PSC").ToList().ForEach(sens => sens.Active = false);
                        }
                        else
                        {
                            sg.Sensors.Where(sens => sens.DeviceType == "PSC").ToList().ForEach(sens => sens.Active = true);
                            LB_PPortStatus.Text = "Sensores Puerto Paralelo: Conectados.";
                            sg.IsPPortActive = true;
                        }
                        factory.Close();                       
                    }
                    catch (Exception e)
                    {
                        sg.IsPPortActive = false;
                        LB_PPortStatus.Text = "Sensores Puerto Paralelo: Desconectados.";
                        sg.Sensors.Where(sens => sens.DeviceType == "PSC").ToList().ForEach(sens => sens.Active = false);
                    }
                }
            }
        }
        //el boton iniciar detetector toma el estado de checkeo de los detectores
        //realiza la accion de iniciar los detectores seleccionados
        private void bt_startDetector_Click(object sender, EventArgs e)
        {            
            StartSensors(ls_detectors.GetItemChecked(0)?"DTMF":"");
            StartSensors(ls_detectors.GetItemChecked(1) ? "PPort" : "");           
            ReadSensorsStatus();
        }
        //el boton detener detetector toma el estado de checkeo de los detectores
        //realiza la accion de iniciar los detectores seleccionados
        private void bt_stopDetector_Click(object sender, EventArgs e)
        {
            StopSensors(ls_detectors.GetItemChecked(0) ? "DTMF" : "");
            StopSensors(ls_detectors.GetItemChecked(1) ? "PPort" : "");
            ReadSensorsStatus();
        }
    }
}
