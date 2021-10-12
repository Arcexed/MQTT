#region

using Microsoft.EntityFrameworkCore;
using MQTT.Data.Entities;

#pragma warning disable 8618
// ReSharper disable InconsistentNaming

#endregion

namespace MQTT.Data
{
    public class MQTTDbContext : DbContext
    {
        public MQTTDbContext(DbContextOptions<MQTTDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<EventsUser> EventsUsers { get; set; }
        public DbSet<EventsDevice> EventsDevices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}