#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MQTT.Api.Contracts.v1.Request;
using MQTT.Api.Options;
using MQTT.Api.Repository;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using Swashbuckle.AspNetCore.Annotations;
using static MQTT.Api.Options.ApiRoutes.Measurements;
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
        [Route(ApiRoutes.Measurements.Create)]
        public IActionResult AddMeasurements(CreateMeasurementRequest request)
        {
            var device = _measurementService.GetDeviceById(request.DeviceId);
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
                return Ok(_measurementService.InsertMeasurement(measurement));
                
            }
            return NotFound("Device does not exists");
        }

        [Authorize]
        [HttpGet(ApiRoutes.Measurements.Get)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<MeasurementViewModel>), 200)]
        public IActionResult GetMeasurements(GetMeasurementRequest request)
        {
            if (request.Limit > 1000) return BadRequest("Limit must be less than 1000");

            if (request.StartDate > request.EndDate) return BadRequest("Start date must be greater than end date");

            if (request.Page < 1) return BadRequest("Page must be greater than 1");
            var device = _measurementService.GetDeviceById(request.DeviceId);
            var user = _measurementService.GetUserById(Guid.Parse(User.Identity.Name));
            var measurements = _measurementService.GetMeasurementsByDevice(device, user, request.StartDate, request.EndDate, request.Page, request.Limit);
            return Ok(measurements);
        }
    }
}