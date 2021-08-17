using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MQTTWebApi.Models;
    
namespace MQTTWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly MqttdbContext _db;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, MqttdbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet("{deviceName}/Minutely")]
        public ReportViewModel MinutelyInfoGET(string deviceName)
        {

            var measurements = _db.Measurements.Where(m =>
                m.Device.Name == deviceName && m.Date >= DateTime.Now.AddMinutes(-1));
            var device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }

        [HttpGet("{deviceName}/Hourly")]
        public ReportViewModel HourlyInfoGET(string deviceName)
        {


            var measurements = _db.Measurements.Where(m => m.Device.Name == deviceName && m.Date >= DateTime.Now.AddHours(-1));
            var device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }


        [HttpGet("{deviceName}/Daily")]
        public ReportViewModel DailyInfoGET(string deviceName)
        {

            var measurements =
                _db.Measurements.Where(m => m.Device.Name == deviceName && m.Date >= DateTime.Now.AddDays(-1));
            var device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }
        [HttpGet("{deviceName}/Monthly")]
        public ReportViewModel MonthlyInfoGET(string deviceName)
        {
            var measurements = _db.Measurements.Where(m => m.Device.Name == deviceName && m.Date > DateTime.Now.AddMonths(-1));
            var device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }
    }
}
