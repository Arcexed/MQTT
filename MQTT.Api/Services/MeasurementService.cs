using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MQTT.Api.Contracts.v1.Request;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    
   
    public class MeasurementService : IMeasurementService
    {
        private readonly MQTTDbContext _db;
        private readonly IMapper _mapper;

        public MeasurementService(MQTTDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _db.Users.FirstOrDefaultAsync(d => d.Id == userId);
        }

        public async Task<Device?> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _db.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
        }
        
        public async Task<IEnumerable<MeasurementViewModel>?> GetMeasurementsByDeviceAsync(Device device, User user,
            DateTime? startDate, DateTime? endDate, int page, int limit)
        {
            var measurements = _db.Measurements
                .Where(d => d.Device == device && (endDate != null && startDate != null
                    ? d.Date > startDate && d.Date < endDate
                    : true) && d.Device.User == user)
                .Skip(limit * (page - 1))
                .OrderByDescending(d => d.Date)
                .Take(limit);
            var measurementViewModels =
                _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>
                    (measurements);
            return measurementViewModels;
        }
        public async Task<MeasurementViewModel> InsertMeasurement(Measurement measurement)
        {
            await _db.Measurements.AddAsync(measurement);
            await Save();
            return _mapper.Map<MeasurementViewModel>(measurement);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}