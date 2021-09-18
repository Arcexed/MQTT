using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Models.Static;

#nullable disable

namespace Models.Models
{
    public partial class Device
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? EditingDate { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }
        public string? PublicIp { get; set; }
        public string? PrivateIp { get; set; }
        [Required]
        public bool isPublic { get; set; }
        public  User User { get; set; }
        public ICollection<EventsDevice> EventsDevices { get; set; }
        public ICollection<Measurement> Measurements { get; set; }

        public string GetVisible()
        {
            if (isPublic)
            {
                return "Public";
            }
            return "Private";
        }

        public string GetStatus()
        {
            if (Measurements != null)
            {
                if (DateTime.Now.Subtract(Measurements.Last().Date).Minutes < 5)
                {
                    return "Online";
                }

                return "Offline";
            }
            return "Unknown";
        }

        public string GetTimespanStringLastMeasurement()
        {
            if (Measurements != null)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(Measurements.Last().Date);
                return Measurements.Last().Date.TimeAgo();
            }
            return "Unknown";

        }

    }
}
