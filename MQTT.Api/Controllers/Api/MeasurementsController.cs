#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQTT.Api.Contracts.v1.Request;
using MQTT.Api.Options;
using MQTT.Api.Repository;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using Swashbuckle.AspNetCore.Annotations;
using static MQTT.Api.Options.ApiRoutes;
// ReSharper disable NotAccessedField.Local

#endregion

namespace MQTT.Api.Controllers.Api
{
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementsController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        public Guid UserId => Guid.Parse(User.Identity?.Name!);


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MeasurementViewModel), 200)]
        [SwaggerOperation("Create Measurement")]
        [Route(Measurements.Create)]
        public async Task<IActionResult> AddMeasurements([FromRoute] Guid deviceId, CreateMeasurementRequest request)
        {
            var device = await _measurementService.GetDeviceByIdAsync(deviceId);
            if (device != null)
            {
                var measurement = new Measurement()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Device = device,
                    Temperature = request.Temperature,
                    AirHumidity = request.AirHumidity,
                    AtmosphericPressure = request.AtmosphericPressure,
                    LightLevel = request.LightLevel,
                    RadiationLevel = request.RadiationLevel,
                    SmokeLevel = request.SmokeLevel,
                };
                var returnedMeasurement = await _measurementService.InsertMeasurement(measurement);
                return Ok(returnedMeasurement);
                
            }
            return NotFound("Device does not exists");
        }

        [Authorize]
        [HttpGet(Measurements.Get)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<MeasurementViewModel>), 200)]
        public async Task<IActionResult> GetMeasurements([FromRoute] Guid deviceId, GetMeasurementRequest request)
        {
            if (request.Limit > 1000) return BadRequest("Limit must be less than 1000");

            if (request.StartDate > request.EndDate) return BadRequest("Start date must be greater than end date");

            if (request.Page < 1) return BadRequest("Page must be greater than 1");

            var device = await _measurementService.GetDeviceByIdAsync(deviceId);
            var user = await _measurementService.GetUserByIdAsync(UserId);
            var measurements = await _measurementService.GetMeasurementsByDeviceAsync(device, user, request.StartDate, request.EndDate, request.Page, request.Limit);
            return Ok(measurements);
        }
    }
}