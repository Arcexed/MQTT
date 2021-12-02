#region

using System;
using System.Text.Json.Serialization;
using AutoMapper;
using MQTT.Data;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Shared.DBO
{
    public class ReportViewModel
    {
        [JsonIgnore] public DeviceViewModel Device { get; set; }


        public static ReportViewModel GenerateReportMeasurements(DeviceViewModel deviceVm, MQTTDbContext db,
            DateTime startDate, IMapper mapper)
        {
            if (deviceVm == null) return null;

            var report = new ReportViewModel
            {
                Device = deviceVm
            };

            return report;
        }

        #region Temp

        public decimal MinTemp { get; set; }
        public decimal AvgTemp { get; set; }
        public decimal MaxTemp { get; set; }

        #endregion

        #region Atm

        public decimal MinAtmosphericPressure { get; set; }
        public decimal AvgAtmosphericPressure { get; set; }
        public decimal MaxAtmosphericPressure { get; set; }

        #endregion

        #region Humid

        public decimal MinAirHumidity { get; set; }
        public decimal AvgAirHumidity { get; set; }
        public decimal MaxAirHumidity { get; set; }

        #endregion

        #region LightLevel

        public decimal MinLightLevel { get; set; }
        public decimal AvgLightLevel { get; set; }
        public decimal MaxLightLevel { get; set; }

        #endregion

        #region Smoke

        public decimal MinSmokeLevel { get; set; }
        public decimal AvgSmokeLevel { get; set; }
        public decimal MaxSmokeLevel { get; set; }

        #endregion
    }
}