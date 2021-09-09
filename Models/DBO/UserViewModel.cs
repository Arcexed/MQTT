using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models.DBO
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }  
        public bool IsBlock { get; set; }
        public string AccessToken { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Device> Devices { get; set; }

    }
}
