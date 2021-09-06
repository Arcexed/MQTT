using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.DbModels;

namespace Models.DBO
{
    public class EventUserViewModel
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
