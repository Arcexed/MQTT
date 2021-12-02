using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<DeviceViewModel> GetDevices(Guid userId)
        {
            var user = _db.Users.Include(d => d.Devices).First(x => x.Id == userId);
            var devices = user.Devices;
            devices.ForAll(x => x.TakeMeasurements(3).TakeEvents(3));
            var devicesView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
            return devicesView.ToList();
        }

        public DeviceViewModel? GetDeviceById(Guid deviceId, Guid userId)
        {
            DeviceViewModel deviceViewModel =
                _mapper.Map<Device?, DeviceViewModel>(_db.Users
                    .FirstOrDefault(x => x.Id == userId)?.Devices.FirstOrDefault(x => x.Id == deviceId)
                    ?.TakeMeasurements(3).TakeEvents(3));
            return deviceViewModel;
        }

        public string InsertDevice(Device device, Guid userId)
        {
            if (!_db.Devices.Any(d => d.Name == device.Name))
            {
                device.Id = Guid.NewGuid();
                device.CreatingDate = DateTime.Now;
                _db.Users.FirstOrDefault(d => d.Id == userId)?.Devices.Add(device);
                Save();
                return "Success";
            }

            return "Device is added";
        }

        public string DeleteDevice(Device device, Guid userId)
        {
            var deviceToDelete = _db.Devices.FirstOrDefault(d => d == device && d.User.Id == userId);
            _db.Devices.Remove(deviceToDelete!);
            Save();
            return "Success";
        }

        public string UpdateDevice(Device device, Guid userId)
        {
            var currentDevice = _db.Devices.FirstOrDefault(d => d.Id == device.Id && d.User.Id == userId);
            if (currentDevice != null)
            {
                currentDevice.EditingDate = DateTime.Now;
                currentDevice.IsPublic = device.IsPublic;
                currentDevice.MqttToken = device.MqttToken;
                currentDevice.Geo = device.Geo;
                currentDevice.Description = device.Description;
                Save();
                return "Success updating";
            }
            return "Failed updating";
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}