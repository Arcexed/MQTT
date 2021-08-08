using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Events
    {
        [Key]
        public string Id { get; set; }
        [Key] [Column("id_device")] public Device Device { get; set; }
        public string Message { get; set; }
    }
}
