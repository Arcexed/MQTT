using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Api.Contracts.v1.Request.DeviceController
{
    public class UpdateDeviceRequest 
    {
         public string Geo { get; set; }
         public string Description { get; set; }
    }
}