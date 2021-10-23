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
        public DbSet<Event> Events { get; set; }
    }

    public static class Test
    {
        private static string cs;
        public static  string CS 
        {
            get => cs;
            set
            {
                cs = value;
                Options = new DbContextOptionsBuilder<MQTTDbContext>()
                    .UseSqlServer(value)
                    .Options;
            }
        }
        public static DbContextOptions<MQTTDbContext> Options { get; set; }
    }
}