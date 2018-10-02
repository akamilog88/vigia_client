using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace HomeAlarm.DAL
{
    public class EventType
    {
        public  const string EventType_Enabled = "Enabled";
        public  const string EventType_Disabled = "Disabled";
        public  const string EventType_Detection = "Detection";

        public int EventTypeId{get;set;}
        public String EventName{get;set;}
    }

    public class EventTypeConfiguration : EntityTypeConfiguration<EventType> { 
        public EventTypeConfiguration(){
            this.ToTable("C_EventType");
            Property(d => d.EventTypeId).HasColumnName("cod_event");
            Property(d => d.EventName).HasColumnName("nom_event");
        }    
    }
}
