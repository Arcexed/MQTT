using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MQTTWebApi.Static;

namespace MQTTWebApi.Models
{
    public class Measurements
    {
        [Key] public Guid Id { get; set; }
        [Key] [Column("id_device")] public Device Device { get; set; }
        public DateTime Time { get; set; }
        public float AtmosphericPressure { get; set; }
        public float Temperature { get; set; }
        public float AirHumidity { get; set; }
        public float LightLevel { get; set; }
        public float SmokeLevel { get; set; }

        public Measurements(string deviceId,float atmosphericPressure,float temperature,float airHumidity,float lightLevel, float smokeLevel)
        {
            using MqttDBContext db = new MqttDBContext();

            this.Device = db.Device.Where(d=>d.Id.ToString()==deviceId).First();
            this.Time = DateTime.Now;
            this.AtmosphericPressure = atmosphericPressure;
            this.Temperature = temperature;
            this.AirHumidity = airHumidity;
            this.LightLevel = lightLevel;
            this.SmokeLevel = smokeLevel;
        }

    }
}
