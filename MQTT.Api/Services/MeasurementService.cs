using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        
        public User? GetUserById(Guid userId)
        {
            return _db.Users.FirstOrDefault(d => d.Id == userId);
        }

        public Device? GetDeviceById(Guid deviceId)
        {
            return _db.Devices.FirstOrDefault(d => d.Id == deviceId);
        }
        
        public IEnumerable<MeasurementViewModel>? GetMeasurementsByDevice(Device device, User user,
            DateTime? startDate, DateTime? endDate, int page, int limit)
        {
            var measurementViewModels =
                    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>
                    (_db.Measurements
                        .Where(d => d.Device == device && (endDate != null && startDate != null
                            ? d.Date > startDate && d.Date < endDate
                            : true) && d.Device.User == user)
                        .Skip(limit * (page - 1))
                        .OrderByDescending(d => d.Date)
                        .Take(limit));
                return measurementViewModels;
        }
        public MeasurementViewModel InsertMeasurement(Measurement measurement)
        {
            _db.Measurements.Add(measurement);
            Save();
            return _mapper.Map<MeasurementViewModel>(measurement);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}