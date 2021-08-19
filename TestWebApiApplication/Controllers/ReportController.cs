using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MQTTWebApi.Models;
using MQTTWebApi.Models.ForReport;

namespace MQTTWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MqttdbContext _db;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, MqttdbContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        [HttpGet("{deviceName}/Minutely")]
        public ReportViewModel MinutelyInfoGET(string deviceName)
        {

            var measurements = _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements.Where(m =>
                m.Device.Name == deviceName && m.Date >= DateTime.Now.AddMinutes(-1)));
            var device = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name == deviceName).FirstOrDefault());
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }

        [HttpGet("{deviceName}/Hourly")]
        public ReportViewModel HourlyInfoGET(string deviceName)
        {


            var measurements = _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements.Where(m =>
                m.Device.Name == deviceName && m.Date >= DateTime.Now.AddHours(-1)));
            var device = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name == deviceName).FirstOrDefault());
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }


        [HttpGet("{deviceName}/Daily")]
        public ReportViewModel DailyInfoGET(string deviceName)
        {

            var measurements = _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements.Where(m =>
                m.Device.Name == deviceName && m.Date >= DateTime.Now.AddDays(-1)));
            var device = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name == deviceName).FirstOrDefault());
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }
        [HttpGet("{deviceName}/Monthly")]
        public ReportViewModel MonthlyInfoGET(string deviceName)
        {
            var measurements = _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements.Where(m =>
                m.Device.Name == deviceName && m.Date >= DateTime.Now.AddMonths(-1)));
            var device = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name == deviceName).FirstOrDefault());
            ReportViewModel report = ReportViewModel.GenerateReportMeasurements(measurements, device);
            return report;

        }
    }
}
