﻿#region

using System;
using System.ComponentModel.DataAnnotations;
using Npgsql.PostgresTypes;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class EventsDevice : IEntity<Guid>
    {
        [Required] public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; } = false;
        [Required] public Guid Id { get; set; }
    }
}