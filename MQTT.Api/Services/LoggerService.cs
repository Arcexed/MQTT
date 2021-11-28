using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
                    Device = _db.Devices.First(d=>d.Id==device.Id)
                };
                _db.EventsDevices.Add(eventDevice);
                _db.SaveChanges();
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
                    User = _db.Users.First(d=>d.Id == user.Id)
                };
                _db.EventsUsers.Add(eventUser);
                _db.SaveChanges();
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("G")} {message}");
                //File.AppendAllText("log.txt", $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} {message}\n");
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