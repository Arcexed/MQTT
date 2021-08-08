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


        [HttpGet("{deviceName}/Minutely")]
        public ReportMeasurements MinutelyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            {
                var measurements = db.Measurements.Where(m =>
                    m.Device.Name == deviceName && m.Time >= DateTime.Now.AddMinutes(-1));
                var device = db.Device.Where(d => d.Name == deviceName).FirstOrDefault();
                ReportMeasurements report =  ReportMeasurements.GenerateReportMeasurements(measurements,device);
                return report;
            }
        }

        [HttpGet("{deviceName}/Hourly")]
        public ReportMeasurements HourlyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            {
                var measurements = db.Measurements.Where(m => m.Device.Name==deviceName && m.Time>=DateTime.Now.AddHours(-1));
                var device = db.Device.Where(d => d.Name == deviceName).FirstOrDefault();
                ReportMeasurements report = ReportMeasurements.GenerateReportMeasurements(measurements, device);
                return report;
            }
        }

        
        [HttpGet("{deviceName}/Daily")]
        public ReportMeasurements DailyInfoGET(string deviceName)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                var measurements =
                    db.Measurements.Where(m => m.Device.Name == deviceName && m.Time >= DateTime.Now.AddDays(-1));
                var device = db.Device.Where(d => d.Name == deviceName).FirstOrDefault();
                ReportMeasurements report = ReportMeasurements.GenerateReportMeasurements(measurements, device);
                return report;
            }
        }
        [HttpGet("{deviceName}/Monthly")]
        public ReportMeasurements MonthlyInfoGET(string deviceName)
        {

            using (MqttDBContext db = new MqttDBContext())
            {
                var measurements = db.Measurements.Where(m => m.Device.Name == deviceName && m.Time > m.Time.AddMonths(-1));
                var device = db.Device.Where(d => d.Name == deviceName).FirstOrDefault();
                ReportMeasurements report = ReportMeasurements.GenerateReportMeasurements(measurements, device);
                return report;
            }
        }
    }
}
