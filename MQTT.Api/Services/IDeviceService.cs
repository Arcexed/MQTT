using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceViewModel>> GetDevicesAsync(Guid userId);
        Task<DeviceViewModel> GetDeviceViewModelByIdAsync(Guid deviceId);
        Task<DeviceViewModel> GetDeviceViewModelByNameAsync(string deviceName);
        Task<Device> GetDeviceByIdAsync(Guid deviceId);
        Task<bool> InsertDeviceAsync(Device device, Guid userId);
        Task<bool> DeleteDeviceAsync(Device device);
        Task<DeviceViewModel> UpdateDeviceAsync(Device device);

        Task<bool> DeviceNameIsExists(string deviceName);
        // OK
        Task<bool> SaveAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<bool> UserOwnsDeviceAsync(Guid deviceId, Guid userId);
    }
}