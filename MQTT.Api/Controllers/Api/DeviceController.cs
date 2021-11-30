﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTT.Api.Models;
using MQTT.Api.Repository;
using MQTT.Api.Services;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTT.Api.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        public Guid UserId => Guid.Parse(User.Identity?.Name!);
        public DeviceController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<DeviceViewModel>), 200)]
        [SwaggerOperation("Get All Device For User")]
        public IActionResult GetAllDevices()
        {
            return Ok(_deviceRepository.GetDevices(UserId));
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DeviceViewModel), 200)]
        [SwaggerOperation("Get Device For User")]
        [Route("{DeviceId}")]
        public IActionResult GetDeviceById([FromBody] Guid DeviceId)
        {
            var deviceViewModel = _deviceRepository.GetDeviceById(DeviceId, UserId);
            return Ok(deviceViewModel);
        }
/*
        [Authorize]
        [HttpPut]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Add Device For User")]
        [Route("Add")]
        public async Task<IActionResult> AddDeviceGet([Required] string name, string desc, string geo)
        {
            try
            {
                var isDuplicateItem = _db.Devices.Any(d => d.Name == name);

                if (isDuplicateItem)
                    return Conflict("This device is exists");
                var user = await _db.Users.FirstOrDefaultAsync(d => d.Username == User.Identity!.Name);
                if (user != null)
                {
                    Device device = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Description = desc,
                        Geo = geo,
                        CreatingDate = DateTime.Now,
                        EditingDate = null,
                        IsPublic = false,
                        MqttToken = Guid.NewGuid().ToString("N")
                    };
                    user.Devices.Add(device);
                    await _db.SaveChangesAsync();
                    _loggerService.Log($"{DateTime.Now.ToString("O")} User {User.Identity?.Name} added device (name:{name}\tdesc:{desc}\tgeo:{geo}) ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                    return Ok("Success adding");
                }

                _loggerService.Log($"{DateTime.Now.ToString("O")} User {User.Identity?.Name} (Not found in database) trying added (fail) device (name:{name}\tdesc:{desc}\tgeo:{geo})  ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPatch]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Edit Device For User")]
        [Route("Edit")]
        public async Task<IActionResult> EditDevice([Required] string name, string desc, string geo)
        {
            if (!string.IsNullOrEmpty(name))
                try
                {
                    var username = User.Identity?.Name;
                    var device = (await _db.Users.FirstOrDefaultAsync(x =>
                        x.Username == username))?.Devices.FirstOrDefault(x => x.Name == name);

                    if (device != null)
                    {
                        if (desc != "")
                            device.Description = desc;

                        if (geo != "")
                            device.Geo = geo;

                        device.EditingDate = DateTime.Now;

                        await _db.SaveChangesAsync();
                        _loggerService.Log($"{DateTime.Now.ToString("O")} User {User.Identity?.Name} (Not found in database user was have {name} device) trying edit (fail) device ({Request.Query}) IP:{HttpContext.Request.Host.Host}");

                        return Ok("Success editing");
                    }

                    _loggerService.Log($"{DateTime.Now.ToString("O")} User {User.Identity?.Name} success edit (fail) device {name} ({desc} {geo}) ({Request.Query}) IP:{HttpContext.Request.Host.Host}");

                    return NotFound("This device is not exists");
                }
                catch (Exception ex)
                {
                    _loggerService.Log($"{ex.Message} {Request.QueryString.Value}");
                    return BadRequest(ex.Message);
                }

            _loggerService.Log($"{DateTime.Now.ToString("O")} User {User.Identity?.Name} (Not found in database) trying added (fail) device (name:{name}\tdesc:{desc}\tgeo:{geo})  ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
            return NotFound("Name is null or empty");
        }

        [Authorize]
        [HttpDelete]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Delete Device For User")]
        [Route("Delete")]
        public IActionResult DeleteDevice([Required] string name)
        {
            if (!string.IsNullOrEmpty(name))
                try
                {
                    var username = User.Identity?.Name;
                    var existsItem = _db.Users.FirstOrDefault(x =>
                        x.Username == username)?.Devices.FirstOrDefault(x => x.Name == name);

                    if (existsItem != null)
                    {
                        _db.Devices.Remove(existsItem);
                        _db.SaveChanges();
                        _loggerService.Log($"User {User.Identity?.Name} success delete device {name} ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                        return Ok("Success delete");
                    }

                    _loggerService.Log($"User {User.Identity?.Name} fail (device is not exists) delete device {name}  ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                    return NotFound("This device is not exists");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            _loggerService.Log($"User {User.Identity?.Name} fail (device name is null or empty) delete device  ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
            return NotFound("Name is null or empty");
        }*/

        
    }
}