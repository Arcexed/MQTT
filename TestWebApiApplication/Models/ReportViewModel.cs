using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTWebApi.Models.ForReport;

namespace MQTTWebApi.Models
{
    public class ReportViewModel
    {

        [JsonIgnore]
        public DeviceViewModel Device { get; set; }
        public MeasurementViewModel? MinTemp { get; set; }
        public float? AvgTemp { get; set; }
        public MeasurementViewModel? MaxTemp { get; set; }
        public MeasurementViewModel? MinAtmosphericPressure { get; set; }
        public float? AvgAtmosphericPressure { get; set; }
        public MeasurementViewModel? MaxAtmosphericPressure { get; set; }
        public MeasurementViewModel? MinAirHumidity { get; set; }
        public float? AvgAirHumidity { get; set; }
        public MeasurementViewModel? MaxAirHumidity { get; set; }
        public MeasurementViewModel? MinLightLevel { get; set; }
        public float? AvgLightLevel { get; set; }
        public MeasurementViewModel? MaxLightLevel { get; set; }
        public MeasurementViewModel? MinSmokeLevel { get; set; }
        public float? AvgSmokeLevel { get; set; }
        public MeasurementViewModel? MaxSmokeLevel { get; set; }

        public static ReportViewModel GenerateReportMeasurements(IEnumerable<MeasurementViewModel> measurements, DeviceViewModel device)
        {
            if (measurements != null && device != null )
            {
                if (measurements.Count() > 0)
                {
                    ReportViewModel report = new ReportViewModel();
                    report.Device = device;
                    report.MinTemp =
                        measurements.FirstOrDefault(x => x.Temperature == measurements.Min(y => y.Temperature));
                    report.AvgTemp = measurements.Average(y => y.Temperature);
                    report.MaxTemp =
                        measurements.FirstOrDefault(x => x.Temperature == measurements.Max(y => y.Temperature));
                    report.MinAtmosphericPressure = measurements.FirstOrDefault(x =>
                        x.AtmosphericPressure == measurements.Min(y => y.AtmosphericPressure));
                    report.AvgAtmosphericPressure = measurements.Average(y => y.AtmosphericPressure);
                    report.MaxAtmosphericPressure = measurements.FirstOrDefault(x =>
                        x.AtmosphericPressure == measurements.Max(y => y.AtmosphericPressure));
                    report.MinAirHumidity =
                        measurements.FirstOrDefault(x => x.AirHumidity == measurements.Min(y => y.AirHumidity));
                    report.AvgAirHumidity = measurements.Average(y => y.AirHumidity);
                    report.MaxAirHumidity =
                        measurements.FirstOrDefault(x => x.AirHumidity == measurements.Min(y => y.AirHumidity));
                    report.MinLightLevel =
                        measurements.FirstOrDefault(x => x.LightLevel == measurements.Min(y => y.LightLevel));
                    report.AvgLightLevel = measurements.Average(y => y.LightLevel);
                    report.MaxLightLevel =
                        measurements.FirstOrDefault(x => x.LightLevel == measurements.Max(y => y.LightLevel));
                    report.MinSmokeLevel =
                        measurements.FirstOrDefault(x => x.SmokeLevel == measurements.Min(y => y.SmokeLevel));
                    report.AvgSmokeLevel = measurements.Average(y => y.SmokeLevel);
                    report.MaxSmokeLevel =
                        measurements.FirstOrDefault(x => x.SmokeLevel == measurements.Max(y => y.SmokeLevel));
                    return report;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
