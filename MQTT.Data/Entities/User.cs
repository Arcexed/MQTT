#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class User
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Email { get; set; }
        public string Ip { get; set; }
        [Required] public bool IsBlock { get; set; }

        [Required] public string AccessToken { get; set; }

        //   public Image Avatar { get; set; } 

        [Required] public Role Role { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<EventsUser> EventsUsers { get; set; }
        [Required] public Guid Id { get; set; }
    }
}