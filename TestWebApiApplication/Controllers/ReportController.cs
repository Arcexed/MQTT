using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ReportViewModel> MinutelyInfoGET(string deviceName)
        {

            var device = _mapper.Map<Device, DeviceViewModel>(await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync());
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddMinutes(-1), _mapper);
                return report;
            }
            else
            {
                return null;
            }

        }

        [HttpGet("{deviceName}/Hourly")]
        public async Task<ReportViewModel> HourlyInfoGET(string deviceName)
        {


            var device = _mapper.Map<Device, DeviceViewModel>(await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync());
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddHours(-1), _mapper);
                return report;
            }
            else
            {
                return null;
            }
        }


        [HttpGet("{deviceName}/Daily")]
        public async Task<ReportViewModel> DailyInfoGET(string deviceName)
        {

            var device = _mapper.Map<Device, DeviceViewModel>(await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync());
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddDays(-1), _mapper);
                return report;
            }
            else
            {
                return null;
            }

        }
        [HttpGet("{deviceName}/Monthly")]
        public async Task<ReportViewModel> MonthlyInfoGET(string deviceName)
        {
            var device = _mapper.Map<Device, DeviceViewModel>(await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync());
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddMonths(-1), _mapper);
                return report;
            }
            else
            {
                return null;
            }

        }
    }
}
