#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Shared.DBO;

// ReSharper disable NotAccessedField.Local

#endregion

namespace MQTT.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<ReportController> _logger;
        private readonly IMapper _mapper;

        public ReportController(ILogger<ReportController> logger, MQTTDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        private DeviceViewModel? FindUserAsync(string deviceName)
        {
            var username = User.Identity?.Name;
            var device = _db.Users.FirstOrDefault(x => x.Username == username)?.Devices
                .FirstOrDefault(x => x.Name == deviceName);
            return _mapper.Map<DeviceViewModel>(device);
        }

        [Authorize]
        [HttpGet("{deviceName}/Minutely")]
        public async Task<ReportViewModel?> MinutelyInfoGet([Required]string deviceName)
        {
            var device = FindUserAsync(deviceName);
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddMinutes(-1), _mapper);
                return report;
            }

            return null;
        }

        [Authorize]
        [HttpGet("{deviceName}/Hourly")]
        public async Task<ReportViewModel?> HourlyInfoGet([Required] string deviceName)
        {
            var device = FindUserAsync(deviceName);
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddHours(-1), _mapper);
                return report;
            }

            return null;
        }

        [Authorize]
        [HttpGet("{deviceName}/Daily")]
        public async Task<ReportViewModel?> DailyInfoGet([Required] string deviceName)
        {
            var device = FindUserAsync(deviceName);
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddDays(-1), _mapper);
                return report;
            }

            return null;
        }

        [Authorize]
        [HttpGet("{deviceName}/Monthly")]
        public async Task<ReportViewModel?> MonthlyInfoGet([Required] string deviceName)
        {
            var device = FindUserAsync(deviceName);
            if (device != null)
            {
                ReportViewModel report =
                    await ReportViewModel.GenerateReportMeasurements(device, _db, DateTime.Now.AddMonths(-1), _mapper);
                return report;
            }

            return null;
        }
    }
}