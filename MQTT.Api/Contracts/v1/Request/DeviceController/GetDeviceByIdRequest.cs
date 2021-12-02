using System;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Api.Contracts.v1.Request.DeviceController
{
    public class GetDeviceByIdRequest
    {
        [FromRoute]
        public Guid DeviceId { get; set; }
    }
}