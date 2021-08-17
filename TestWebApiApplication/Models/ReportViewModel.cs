using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MQTTWebApi.Models.ForReport;

namespace MQTTWebApi.Models
{
    public class ReportViewModel
    {
        public Device Device { get; set; }
        public MeasurementViewModel MinTemp { get; set; }
        public MeasurementViewModel AvgTemp { get; set; }
        public MeasurementViewModel MaxTemp { get; set; }
        public MeasurementViewModel MinAtmosphericPressure { get; set; }
        public float AvgAtmosphericPressure { get; set; }
        public MeasurementViewModel MaxAtmosphericPressure { get; set; }
        public MeasurementViewModel MinAirHumidity { get; set; }
        public float AvgAirHumidity { get; set; }
        public MeasurementViewModel MaxAirHumidity { get; set; }
        public MeasurementViewModel MinLightLevel { get; set; }
        public float AvgLightLevel { get; set; }
        public MeasurementViewModel MaxLightLevel { get; set; }
        public MeasurementViewModel MinSmokeLevel { get; set; }
        public float AvgSmokeLevel { get; set; }
        public MeasurementViewModel MaxSmokeLevel { get; set; }

        public static ReportViewModel GenerateReportMeasurements(IQueryable<Measurement> measurements, Device device)
        {
            if (measurements != null && device != null)
            {

                ReportViewModel report = new ReportViewModel();
                report.Device = device;
                //report.MinTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Min(y => y.Temperature));
                //report.AvgTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Average(y => y.Temperature));
                //report.MaxTemp = measurements.FirstOrDefault(x => x.Temperature == measurements.Max(y => y.Temperature));
                return report;
            }
            else
            {
                return null;
            }
        }

    }
}
