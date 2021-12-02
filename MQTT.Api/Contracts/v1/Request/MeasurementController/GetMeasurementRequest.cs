using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Api.Contracts.v1.Request
{
    public class GetMeasurementRequest
    {
        [FromRoute]
        public Guid DeviceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 25;
    }
}