#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

#pragma warning disable 8618
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace MQTT.Data.Entities
{
    public class Device : IEntity<Guid>
    {
        [Required] public string Name { get; set; }
        
        [Required] public DateTime CreatingDate { get; set; }
        public DateTime? EditingDate { get; set; }
        public string Geo { get; set; }
        public string Description { get; set; }
        public string? PublicIp { get; set; }
        public string? PrivateIp { get; set; } 

        [Required] public string MqttToken { get; set; }
        [Required] public bool IsPublic { get; set; }
        public ICollection<EventsDevice> EventsDevices { get; set; }
        public ICollection<Measurement> Measurements { get; set; }

        public string Visible =>
            IsPublic ? "Public" : "Private";

        public string Status =>
            Measurements == null ? "Unknown" :
            DateTime.Now.Subtract(Measurements.Last().Date).Minutes < 5 ? "Online" : "Offline";

        public string TimespanStringLastMeasurement =>
            Measurements == null ? "Unknown" : Measurements.Last().Date.ToString(CultureInfo.CurrentCulture);

        [Required] public Guid Id { get; set; } 

        public Device TakeMeasurements(int count)
        {
            Measurements = Measurements.Take(count).ToList();
            return this;
        }

        public Device TakeEvents(int count)
        {
            EventsDevices = EventsDevices.Take(count).ToList();
            return this;
        }
    }
}