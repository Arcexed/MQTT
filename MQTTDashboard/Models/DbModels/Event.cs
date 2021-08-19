using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#nullable disable

namespace MQTTWebApi.Models
{

    public partial class Event
    {
        public Guid Id { get; set; }
        public Guid IdDevice { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public virtual Device Device { get; set; }
    }
}
