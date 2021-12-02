using System;
using System.Collections.Generic;
using MQTT.Api.Contracts.v1.Request;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public interface IMeasurementService
    {
        IEnumerable<MeasurementViewModel>? GetMeasurementsByDevice(Device device, User user, DateTime? startDate,
            DateTime? endDate, int page, int limit);

        public User? GetUserById(Guid userId);
        public Device? GetDeviceById(Guid deviceId);

        MeasurementViewModel InsertMeasurement(Measurement measurement);
        
        void Save();
    }
}