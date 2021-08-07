using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MQTTWebApi.Models;
using MQTTWebApi.Static;

namespace MQTTWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportMeasurementsController : ControllerBase
    {
        
        private readonly ILogger<ReportMeasurementsController> _logger;

        public ReportMeasurementsController(ILogger<ReportMeasurementsController> logger)
        {
            _logger = logger;
        }


        [HttpGet("Minutely")]
        public IEnumerable<Measurements> MinutelyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            {

                return db.Measurements.Where(m => m.Device.Name == deviceName && m.Time >= DateTime.Now.AddMinutes(-1));
            }
        }

        [HttpGet("Hourly")]
        public IEnumerable<Measurements> HourlyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            {
        
                return db.Measurements.Where(m => m.Device.Name==deviceName && m.Time>=DateTime.Now.AddHours(-1));
            }
        }

        private ReportMeasurements AvgMinuteMeasurements(string deviceName)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                var measurements =
                    db.Measurements.Where(m => m.Device.Name == deviceName && m.Time >= DateTime.Now.AddMinutes(-1));
                if (measurements.Count() > 0)
                {
                    var MeasurementTempMin = measurements.FirstOrDefault(x => x.Temperature == measurements.Min(y => y.Temperature));

                    //ReportMeasurements report = MeasurementTempMin;
                   

                    return null;

                }
                else
                {
                    return null;
                }

            }

            return null;
        }
        [HttpGet("Daily")]
        public IEnumerable<Measurements> DailyInfoGET(string deviceName)
        {
            return null;
            //using (MqttDBContext db = new MqttDBContext())
            //{
            //    return db.Measurements.Where(m => m.Device.Name == deviceName && DateTime.Now - m.Time);
            //}

        }
        [HttpGet("Monthly")]
        public IEnumerable<Measurements> MonthlyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            { 
                return db.Measurements.Where(m => m.Device.Name == deviceName && m.Time > m.Time.AddMonths(-1));
            }
        }
    }
}
