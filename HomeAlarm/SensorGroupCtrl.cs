using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HomeAlarm
{
    public partial class SensorGroupCtrl : UserControl
    {
        public string Code { private set; get; }
        public string CodeName { private set; get; }
        public string DesciptiveName { private set; get; }
        public bool isLocal { private set; get; }
        public bool hasDTMFS { private set; get; }
        public bool hasPPort { private set; get; }
        public Uri dtmf_Address { private set; get; }
        public Uri pport_Address { private set; get; }
        private List<SensorUserCtrl> ls;
        public List<SensorUserCtrl> Sensors { get { return ls; } }
        public bool IsConnected{get;set;}
        public bool IsDTMFActive { get; set; }
        public bool IsPPortActive { get; set; }


        public SensorGroupCtrl(string code, string pname, string dname, bool local, bool dtmf, bool pport, Uri pport_address=null, Uri dtmf_address=null)
        {
            InitializeComponent();
            ls = new List<SensorUserCtrl>();
            this.Code = code;
            CodeName = pname;
            DesciptiveName = dname;
            isLocal = local;
            hasDTMFS = dtmf;
            hasPPort = pport;
            IsConnected = true;
            this.pport_Address = pport_address;
            this.dtmf_Address = dtmf_address;
            IsDTMFActive = false;
            IsPPortActive = false;
        }
        
        public void Reset() {
            this.IsConnected = true;
            lastContact = DateTime.Now;
            foreach (var s in ls)
            {
                s.Active = true;
                s.Fired = false;
            }
        }
        public DateTime lastContact { get; set; }
        public void AddSensor(SensorUserCtrl sensor) {
            this.FL_sensorContainer.Controls.Add(sensor);
            this.Sensors.Add(sensor);
        }
    }
}
