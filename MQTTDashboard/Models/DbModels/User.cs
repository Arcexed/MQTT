using System;
using System.Collections.Generic;

#nullable disable

namespace MQTTDashboard.Models.DbModels
{
    public partial class User
    {
        public User()
        {
            Devices = new HashSet<Device>();
            EventsUser = new HashSet<EventUser>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public bool IsBlock { get; set; }
        public string AccessToken { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<EventUser> EventsUser { get; set; }
    }
}
