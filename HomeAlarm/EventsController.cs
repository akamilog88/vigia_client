using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeAlarm.DAL;
using System.Xml;
using System.Xml.Linq;

namespace HomeAlarm.Controllers
{
    public class EventsController
    {
        private AlarmContext context;
        public EventsController(AlarmContext context)
        {
            this.context = context;
        }

        public void RegisterNewEvent(Event e, String EName)
        {
                EventType ty = context.EventTypes.SingleOrDefault(t => t.EventName == EName);
                if (ty == null)
                {
                    ty = new EventType { EventName = EName };
                    context.EventTypes.Add(ty);
                }
                e.Type = ty;
                context.Events.Add(e);
                context.SaveChanges();
        }

        public List<Event> ObtenerEventos(DateTime inicio, DateTime fin) {
            List<Event> evts = new List<Event>();
            if (fin == inicio)
            {
                inicio = inicio.Subtract(new TimeSpan(12, 0, 0));
                fin= DateTime.Now ;
                fin = fin.AddHours(12);
            }
                //evts = context.Events.Include("SensorsGroup").Where(e => e.Date >= inicio && e.Date <= fin && e.Messaje.ToUpper().Contains(message.ToUpper())).OrderBy(e => e.Date).ToList(); 
            evts = context.Events.Include("SensorsGroup").Where(e => e.Reciv_Date >= inicio && e.Reciv_Date <= fin).ToList(); 
            
            return evts;
        }
    }
}
