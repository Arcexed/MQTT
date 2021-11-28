#region

using System;
using System.Collections.Generic;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

#endregion

namespace MQTT.Shared.DBO
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? EditingDate { get; set; }
        public string Geo { get; set; }
        public string Desc { get; set; }
        public IEnumerable<MeasurementViewModel> LastThreeMeasurements { get; set; }
        public IEnumerable<EventDeviceViewModel> LastThreeEvents { get; set; }
    }
}