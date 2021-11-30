using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Api.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly MQTTDbContext _db;
        private readonly IMapper _mapper;
        public DeviceRepository(MQTTDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IEnumerable<DeviceViewModel> GetDevices(Guid UserId)
        {
            var user = _db.Users.Include(d => d.Devices).First(x => x.Id == UserId);
            var devices = user.Devices;
            devices.ForAll(x => x.TakeMeasurements(3).TakeEvents(3));
            var devicesView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
            return devicesView.ToList();
        }

        public DeviceViewModel? GetDeviceById(Guid DeviceId, Guid UserId)
        {
            DeviceViewModel deviceViewModel =
                _mapper.Map<Device?, DeviceViewModel>(_db.Users
                    .FirstOrDefault(x => x.Id == UserId)?.Devices.FirstOrDefault(x => x.Id == DeviceId)
                    ?.TakeMeasurements(3).TakeEvents(3));
            return deviceViewModel;
        }

        public string InsertDevice(Device Device, Guid UserId)
        {
            if (!_db.Devices.Any(d => d.Name == Device.Name))
            {
                Device.Id = Guid.NewGuid();
                Device.CreatingDate = DateTime.Now;
                _db.Users.FirstOrDefault(d=>d.Id==UserId)?.Devices.Add(Device);
                Save();
                return "Success";
            }
            return "Device is added";
        }

        public string DeleteDevice(Guid DeviceId, Guid UserId)
        {
            var device = _db.Devices.FirstOrDefault(d => d.Id == DeviceId && d.User.Id == UserId);
            _db.Devices.Remove(device!);
            Save();
            return "Success";
        }

        public string UpdateDevice(Device Device, Guid UserId)
        {
            var currentDevice = _db.Devices.FirstOrDefault(d => d.Id == Device.Id && d.User.Id == UserId);
            if (currentDevice != null)
            {
                currentDevice.EditingDate = DateTime.Now;
                currentDevice.IsPublic = Device.IsPublic;
                currentDevice.MqttToken = Device.MqttToken;
                currentDevice.Geo = Device.Geo;
                currentDevice.Description = Device.Description;
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