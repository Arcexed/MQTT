using System;
using System.Collections.Generic;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public interface IDeviceService
    {
        IEnumerable<DeviceViewModel> GetDevices(Guid userId);
        DeviceViewModel? GetDeviceById(Guid deviceId, Guid userId);
        string InsertDevice(Device device, Guid userId);
        string DeleteDevice(Device device, Guid userId);
        string UpdateDevice(Device device, Guid userId);
        void Save();
    }
}