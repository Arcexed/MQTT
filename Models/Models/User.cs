using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable

namespace Models.Models
{
    public partial class User
    {

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public bool IsBlock { get; set; }
        public string AccessToken { get; set; }
     //   public Image Avatar { get; set; } 
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<EventsUser> EventsUsers { get; set; }
    }
}
