using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace HomeAlarm.DAL
{
    public class SensorsGroup
    {
        public int SensorsGroupId{get;set;}
        public string Address { get; set; }
        public string Name { get; set; }
        public string DescriptiveName { get; set; }
    }
    public class SensorsGroupConfiguration : EntityTypeConfiguration<SensorsGroup>
    {
        public SensorsGroupConfiguration()
        {
            this.ToTable("C_SensorsGroup");
            this.Property(o => o.DescriptiveName).HasColumnName("Descipcion");
        }
    }
}
