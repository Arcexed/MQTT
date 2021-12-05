using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public class DeviceService : IDeviceService
    {
        private readonly MQTTDbContext _db;
        private readonly IMapper _mapper;

        public DeviceService(MQTTDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeviceViewModel>> GetDevicesAsync(Guid userId)
        {
            // TODO: FIX BUG (DEVICE DOES NOT RETURN LASTTHREE MEASUREMENTS AND EVENTS)
            var user = await _db.Users.Include(d => d.Devices).FirstAsync(x => x.Id == userId);
            var devices = user.Devices;
            devices.ForAll(x => x.TakeMeasurements(3).TakeEvents(3));
            var devicesView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
            return devicesView.ToList();
        }
        

       

        public async Task<DeviceViewModel> GetDeviceViewModelByIdAsync(Guid deviceId)
        {
            var device = await _db.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
            DeviceViewModel deviceViewModel =
                _mapper.Map<Device?, DeviceViewModel>(device);
            return deviceViewModel;
        }

        public async Task<DeviceViewModel> GetDeviceViewModelByNameAsync(string deviceName)
        {
            var device = await _db.Devices.FirstOrDefaultAsync(d => d.Name == deviceName);
            var deviceViewModel = _mapper.Map<Device, DeviceViewModel>(device);
            return deviceViewModel;
        }

        public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
        {
            var device = await _db.Devices.FirstOrDefaultAsync(d => d.Id == deviceId);
            return device;
        }

        public async Task<bool> InsertDeviceAsync(Device device, Guid userId)
        {
            var user = await _db.Users.Include(d=>d.Devices).FirstAsync(d => d.Id == userId);
            device.Id = Guid.NewGuid();
            device.CreatingDate = DateTime.Now;
            device.User = user;
            device.MqttToken = Guid.NewGuid().ToString();
            _db.Devices.Add(device);
            return await SaveAsync();;

        }

        public async Task<bool> DeleteDeviceAsync(Device device)
        {
            _db.Devices.Remove(device);
            return await SaveAsync();
        }
        
        
        // MAYBE OK
        public async Task<DeviceViewModel> UpdateDeviceAsync(Device device)
        {
            device.EditingDate = DateTime.Now;
            _db.Devices.Update(device);
            await SaveAsync();
            return _mapper.Map<Device, DeviceViewModel>(device);
        }

        public async Task<bool> DeviceNameIsExists(string deviceName)
        {
            var result = await _db.Devices.AnyAsync(d => d.Name == deviceName);
            return result;
        }


        // OK
        public async Task<bool> SaveAsync()
        {
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        
        
        // OK
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _db.Users.FirstOrDefaultAsync(d => d.Id == userId);
        }

        
        
        // OK
        public async Task<bool> UserOwnsDeviceAsync(Guid deviceId, Guid userId)
        {
            var device = await _db.Devices.FirstOrDefaultAsync(d => d.Id == deviceId && d.User.Id == userId);

            return device != null ? true : false;
        }
    }
}