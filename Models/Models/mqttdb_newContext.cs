using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Models.Models
{
    public class MqttdbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<EventsDevice> EventsDevices { get; set; }
        public DbSet<EventsUser> EventsUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Device> Devices { get; set; }


        public MqttdbContext() : base(GetOptions("Data Source=178.54.86.113, 14330;Initial Catalog=mqttdb_new;User ID=SA;Password=19Andrei19"))
        {

        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

    }
}
