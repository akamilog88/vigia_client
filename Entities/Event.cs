using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace HomeAlarm.DAL
{
    public class Event
    {
        public int EventId{get;set;}
        public DateTime Send_Date { get; set; }
        public DateTime Reciv_Date { get; set; }
        public EventType Type { get; set; }
        public int EventTypeId { get; set; }
        public SensorsGroup SensorsGroup { get; set; }
        public int SensorsGroupId { get; set; }
        public String Code { get; set; }
        public String Messaje { get; set; }
        public String SensorsInfo { get; set; }
        public String DeviceType { get; set; }
    }

    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            this.ToTable("Events");           
            this.HasRequired(et => et.Type);
            this.HasRequired(et => et.SensorsGroup);
        }
    }
}
