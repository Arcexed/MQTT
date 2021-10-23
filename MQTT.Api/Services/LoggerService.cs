using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Data.Entities;

namespace MQTT.Api.Services
{
    public class LoggerService
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(MQTTDbContext db, ILogger<LoggerService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void LogEventDevice(Device device, string message)
        {
            var eventDevice = new EventsDevice()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Message = message,
                IsSeen = false,
                Device = device
            };
            _db.EventsDevices.Add(eventDevice);
            _db.SaveChanges();
        }
        public void LogEventUser(User user, string message)
        {
            var eventUser = new EventsUser()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Message = message,
                IsSeen = false,
                User = user,
            };
            _db.EventsUsers.Add(eventUser);
            _db.SaveChanges();
        }

        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} {message}");
        }

        public void LogEvent(string message)
        {
            Event @event = new Event()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Message = message,
                IsSeen = false,
            };
            _db.Events.Add(@event);
            _db.SaveChanges();
        }

    }

}