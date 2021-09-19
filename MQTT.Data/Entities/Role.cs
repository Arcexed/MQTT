#region

using System;

#pragma warning disable 8618
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class Role : IEntity<Guid>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}