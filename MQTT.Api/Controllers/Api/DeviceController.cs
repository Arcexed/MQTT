using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQTT.Api.Contracts.v1.Request.DeviceController;
using MQTT.Api.Options;
using MQTT.Api.Repository;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using Swashbuckle.AspNetCore.Annotations;
using static MQTT.Api.Options.ApiRoutes;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTT.Api.Controllers.Api
{
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        private Guid UserId => Guid.Parse(User.Identity?.Name!);

        [HttpGet(Devices.GetAll)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<DeviceViewModel>), 200)]
        [SwaggerOperation("Get All Device For User")]
        public async Task<IActionResult> GetAllDevices()
        {
            var devices = await _deviceService.GetDevicesAsync(UserId);
            return Ok(devices);
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DeviceViewModel), 200)]
        [SwaggerOperation("Get Device For User")]
        [Route(Devices.Get)]
        public async Task<IActionResult> GetDeviceById([FromRoute] Guid deviceId, [FromRoute] GetDeviceByIdRequest request)
        {
            var userIsOwnDevice = await _deviceService.UserOwnsDeviceAsync(deviceId, UserId);
            if (userIsOwnDevice)
            {
                var deviceViewModel = await _deviceService.GetDeviceViewModelByIdAsync(deviceId);
                return Ok(deviceViewModel);
            }
            return BadRequest("Not found device");
            
        }   

        [HttpPut]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Add Device For User")]
        [Route(Devices.Create)]
        public async Task<IActionResult> AddDevice(CreateDeviceRequest request)
        {
            var deviceNameIsExists = await _deviceService.DeviceNameIsExists(request.Name);
            if (!deviceNameIsExists)
            {
                Device device = new Device()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Geo = request.Geo,
                    

                };
                var result = await _deviceService.InsertDeviceAsync(device, UserId);
                if (result)
                {
                    var deviceViewModel = await _deviceService.GetDeviceViewModelByNameAsync(request.Name);
                    return Ok(deviceViewModel);
                }
                return BadRequest("Adding unsuccessfully");
            }
            return BadRequest("Device name is exists");
        }

        
        [HttpPatch]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Update Device For User")]
        [Route(Devices.Update)]
        public async Task<IActionResult> UpdateDevice([FromRoute] Guid deviceId, [FromBody] UpdateDeviceRequest request)
        {
            var userIsOwnDevice = await _deviceService.UserOwnsDeviceAsync(deviceId, UserId);
            if (userIsOwnDevice)
            {
                var device = await _deviceService.GetDeviceByIdAsync(deviceId);
                device.Geo = request.Geo;
                device.Description = request.Description;
                
                var updatedDevice = await _deviceService.UpdateDeviceAsync(device);
                return Ok(updatedDevice);
            }
            return NotFound("Not found device");
        }
        
        
        [HttpDelete]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Delete Device For User")]
        [Route(Devices.Delete)]
        public async Task<IActionResult> DeleteDevice([FromRoute] Guid deviceId)
        {
            var userOwnsDevice = await _deviceService.UserOwnsDeviceAsync(deviceId, UserId);
            if (userOwnsDevice)
            {
                var device = await _deviceService.GetDeviceByIdAsync(deviceId);
                var isSuccess = await _deviceService.DeleteDeviceAsync(device);
                if (isSuccess)
                {
                    return Ok("Success delete");
                }
                return BadRequest("Failed deleting");
            }

            return BadRequest("Device does not found");
        }
    }
}