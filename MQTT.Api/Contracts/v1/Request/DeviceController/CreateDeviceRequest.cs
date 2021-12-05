using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using MQTT.Data.Entities;

namespace MQTT.Api.Contracts.v1.Request.DeviceController
{
    public class CreateDeviceRequest
    {
       
        [Required] public string Name { get; set; }

        public string Geo { get; set; }
        public string Description { get; set; }
        public string PublicIp { get; set; }
        public string PrivateIp { get; set; }
        public bool IsPublic { get; set; } = false;

    }
}