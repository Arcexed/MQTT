#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class EventsDevice 
    {
        [Required] public DateTime Date { get; set; }
        
#pragma warning disable 8618
        [Required] public string Message { get; set; }
        [Required] public Device Device { get; set; }

#pragma warning restore 8618

        public bool IsSeen { get; set; } = false;
        
        [Required] public Guid Id { get; set; }

        public override string ToString() => $"{Date.ToString(CultureInfo.CurrentCulture)} {Id} {Device.Name} {Message}";
    }
}