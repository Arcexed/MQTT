#region

using System;
using System.Collections.Generic;

#pragma warning disable 8618
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class User : IEntity<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public bool IsBlock { get; set; }

        public string AccessToken { get; set; }

        //   public Image Avatar { get; set; } 

        public Role Role { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<EventsUser> EventsUsers { get; set; }
        public Guid Id { get; set; }
    }
}