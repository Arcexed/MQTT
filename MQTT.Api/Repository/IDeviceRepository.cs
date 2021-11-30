using System;
using System.Collections;
using System.Collections.Generic;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public interface IDeviceRepository
    {
        IEnumerable<DeviceViewModel> GetDevices(Guid UserId);
        DeviceViewModel? GetDeviceById(Guid DeviceId, Guid UserId);
        string InsertDevice(Device Device, Guid UserId);
        string DeleteDevice(Guid DeviceId, Guid UserId);
        string UpdateDevice(Device Device, Guid UserId);
        void Save();
    }
}