/*using System;
using System.Collections.Generic;
using System.Net;
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

        [HttpGet(ApiRoutes.Device.GetAll)]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<DeviceViewModel>), 200)]
        [SwaggerOperation("Get All Device For User")]
        public IActionResult GetAllDevices()
        {
            return Ok(_deviceService.GetDevices(UserId));
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DeviceViewModel), 200)]
        [SwaggerOperation("Get Device For User")]
        [Route("{deviceId}")]
        public IActionResult GetDeviceById(GetDeviceByIdRequest request)
        {
            var deviceViewModel = _deviceService.GetDeviceById(request.DeviceId, UserId);
            return Ok(deviceViewModel);
        }

        [HttpPut]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Add Device For User")]
        [Route("Add")]
        public IActionResult AddDeviceGet(CreateDeviceRequest device)
        {
            return Ok(_deviceService.InsertDevice(device, UserId));
        }


        [HttpPatch]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Edit Device For User")]
        [Route("Edit")]
        public IActionResult EditDevice([FromBody] Device device)
        {
            return Ok(_deviceService.UpdateDevice(device, UserId));
        }

        [HttpDelete]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Delete Device For User")]
        [Route("Delete")]
        public IActionResult DeleteDevice([FromBody] Device device)
        {
            return Ok(_deviceService.DeleteDevice(device, UserId));
        }
    }
}*/