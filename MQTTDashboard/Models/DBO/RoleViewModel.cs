using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTDashboard.Models.DbModels;

namespace MQTTDashboard.Models
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<User> Users { get; set; }
    }
}
