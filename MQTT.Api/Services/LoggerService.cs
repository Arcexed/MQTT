using System;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Data.Entities;

namespace MQTT.Api.Services
{
    public class LoggerService
    {
        public LoggerService()
        {
        }

        public void LogEventDevice(Device device, string message)
        {
            using (var _db = new MQTTDbContext(Test.Options))
            {

                var eventDevice = new EventsDevice()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Message = message,
                    IsSeen = false,
                    //Device = device
                };
                device.EventsDevices.Add(eventDevice);
                _db.SaveChanges();
                //_db.EventsDevices.Add(eventDevice);
                //_db.SaveChanges();
            }
        }
        public void LogEventUser(User user, string message)
        {
            using (var _db = new MQTTDbContext(Test.Options))
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
        }

        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} {message}");
        }

        public void LogEvent(string message)
        {
            using (var _db = new MQTTDbContext(Test.Options))
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

}