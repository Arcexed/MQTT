using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Events
    {
        [Key]
        public string Id { get; set; }
        public Guid id_device { get; set; }
        public string Message { get; set; }
    }
}
