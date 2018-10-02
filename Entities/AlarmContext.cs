using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace HomeAlarm.DAL
{
    public class AlarmContext:DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<SensorsGroup> SensorGroups { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new EventTypeConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new SensorsGroupConfiguration());
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>(); 
        }
    }
}
