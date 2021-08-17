using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models.ForReport
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
