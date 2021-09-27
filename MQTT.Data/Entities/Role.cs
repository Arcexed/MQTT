#region

using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class Role : IEntity<Guid>
    {
        [Required] public string Name { get; set; }
        [Required] public Guid Id { get; set; }
    }
}