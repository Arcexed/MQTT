using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQTT.Api.Contracts.v1.Request;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public interface IMeasurementService
    {
        Task<IEnumerable<MeasurementViewModel>?> GetMeasurementsByDeviceAsync(Device device, User user, DateTime? startDate,
            DateTime? endDate, int page, int limit);

        public Task<User?> GetUserByIdAsync(Guid userId);
        public Task<Device?> GetDeviceByIdAsync(Guid deviceId);

        Task<MeasurementViewModel> InsertMeasurement(Measurement measurement);
        
        Task Save();
    }
}