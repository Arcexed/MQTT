using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class User
    {
        public User()
        {
            Devices = new HashSet<Device>();
            EventsUsers = new HashSet<EventsUser>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public bool IsBlock { get; set; }
        public string AccessToken { get; set; }
        public Guid IdRole { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<EventsUser> EventsUsers { get; set; }
    }
}
