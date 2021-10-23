#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 8618

#endregion

namespace MQTT.Data.Entities
{
    public class EventsUser
    {
        [Required] public DateTime Date { get; set; }
        
        [Required] public string Message { get; set; }

        public bool IsSeen { get; set; } = false;
        
        [Required] public Guid Id { get; set; }
        [Required] public User User { get; set; }
        public override string ToString() => $"{Date.ToString(CultureInfo.CurrentCulture)} {Id} {User.Username} {Message}";

    }
}