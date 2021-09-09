using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.DbModels;
using Models.DBO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MQTTWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly mqttdb_newContext _db;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, mqttdb_newContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        private DeviceViewModel? FindUserAsync(string deviceName)
        {
            var username = User.Identity.Name;
            var device = _db.Devices.FirstOrDefault(d => d.Name == deviceName && d.IdUserNavigation.Username == username);
            return _mapper.Map<DeviceViewModel>(device);
        }
        [Authorize]
        [HttpGet("{deviceName}/Minutely")]
        public async Task<ReportViewModel?> MinutelyInfoGet(string deviceName)
        {
            var device = FindUserAsync(deviceName);
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

        [Authorize]
        [HttpGet("{deviceName}/Hourly")]
        public async Task<ReportViewModel?> HourlyInfoGet(string deviceName)
        {
            var device = FindUserAsync(deviceName);
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

        [Authorize]
        [HttpGet("{deviceName}/Daily")]
        public async Task<ReportViewModel?> DailyInfoGet(string deviceName)
        {
            var device = FindUserAsync(deviceName);
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
        [Authorize]
        [HttpGet("{deviceName}/Monthly")]
        public async Task<ReportViewModel?> MonthlyInfoGet(string deviceName)
        {

            var device = FindUserAsync(deviceName);
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
