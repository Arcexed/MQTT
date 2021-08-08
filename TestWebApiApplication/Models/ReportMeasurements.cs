using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class ReportMeasurements
    {
        public Device Device { get; set; }
        public Measurements MinTemp { get; set; }
        public Measurements AvgTemp { get; set; }
        public Measurements MaxTemp { get; set; }
        public Measurements MinAtmosphericPressure { get; set; }
        public float AvgAtmosphericPressure { get; set; }
        public Measurements MaxAtmosphericPressure { get; set; }
        public Measurements MinAirHumidity { get; set; }
        public float AvgAirHumidity { get; set; }
        public Measurements MaxAirHumidity { get; set; }
        public Measurements MinLightLevel { get; set; }
        public float AvgLightLevel { get; set; }
        public Measurements MaxLightLevel { get; set; }
        public Measurements MinSmokeLevel { get; set; }
        public float AvgSmokeLevel { get; set; }
        public Measurements MaxSmokeLevel { get; set; }

        public static ReportMeasurements GenerateReportMeasurements(IQueryable<Measurements> measurements, Device device)
        {
            if (measurements != null && device!=null)
            {
                ReportMeasurements report = new ReportMeasurements();
                report.Device = device;
                report.MinTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Min(y => y.Temperature));
                report.AvgTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Average(y => y.Temperature));
                report.MaxTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Max(y => y.Temperature));
                return report;
            }
            else
            {
                return null;
            }
        }

    }
}
