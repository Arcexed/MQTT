using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models.DBO
{
    public class ReportViewModel
    {
        [JsonIgnore]
        public DeviceViewModel Device { get; set; }
        public MeasurementViewModel MinTemp { get; set; }
        public float? AvgTemp { get; set; }
        public MeasurementViewModel MaxTemp { get; set; }
        public MeasurementViewModel MinAtmosphericPressure { get; set; }
        public float? AvgAtmosphericPressure { get; set; }
        public MeasurementViewModel MaxAtmosphericPressure { get; set; }
        public MeasurementViewModel MinAirHumidity { get; set; }
        public float? AvgAirHumidity { get; set; }
        public MeasurementViewModel MaxAirHumidity { get; set; }
        public MeasurementViewModel MinLightLevel { get; set; }
        public float? AvgLightLevel { get; set; }
        public MeasurementViewModel MaxLightLevel { get; set; }
        public MeasurementViewModel MinSmokeLevel { get; set; }
        public float? AvgSmokeLevel { get; set; }
        public MeasurementViewModel MaxSmokeLevel { get; set; }

        public ReportViewModel()
        {
            //this.IdDevice = device;
            //this.MinTemp =mapper.Map<Measurement, MeasurementViewModel>(
            //    _db.Measurements
            //        .Where(d => d.IdDevice.Name.Equals(device.Name))
            //        .Where(x => x.Date >= startDate)   
            //        .Where(d => d.Temperature==await _db.Measurements.Where(d => d.IdDevice.Name.Equals(device.Name)).FirstOrDefaultAsync());
            ////this.AvgTemp = measurements.Average(y => y.Temperature);
            //this.MaxTemp =
            //    measurements.FirstOrDefault(x => x.Temperature == measurements.Max(y => y.Temperature));
            //this.MinAtmosphericPressure = measurements.FirstOrDefault(x =>
            //    x.AtmosphericPressure == measurements.Min(y => y.AtmosphericPressure));
            //this.AvgAtmosphericPressure = measurements.Average(y => y.AtmosphericPressure);
            //this.MaxAtmosphericPressure = measurements.FirstOrDefault(x =>
            //    x.AtmosphericPressure == measurements.Max(y => y.AtmosphericPressure));
            //this.MinAirHumidity =
            //    measurements.FirstOrDefault(x => x.AirHumidity == measurements.Min(y => y.AirHumidity));
            //this.AvgAirHumidity = measurements.Average(y => y.AirHumidity);
            //this.MaxAirHumidity =
            //    measurements.FirstOrDefault(x => x.AirHumidity == measurements.Min(y => y.AirHumidity));
            //this.MinLightLevel =
            //    measurements.FirstOrDefault(x => x.LightLevel == measurements.Min(y => y.LightLevel));
            //this.AvgLightLevel = measurements.Average(y => y.LightLevel);
            //this.MaxLightLevel =
            //    measurements.FirstOrDefault(x => x.LightLevel == measurements.Max(y => y.LightLevel));
            //this.MinSmokeLevel =
            //    measurements.FirstOrDefault(x => x.SmokeLevel == measurements.Min(y => y.SmokeLevel));
            //this.AvgSmokeLevel = measurements.Average(y => y.SmokeLevel);
            //this.MaxSmokeLevel =
            //    measurements.FirstOrDefault(x => x.SmokeLevel == measurements.Max(y => y.SmokeLevel));

        }

        public static async Task<ReportViewModel> GenerateReportMeasurements(DeviceViewModel device, MqttdbContext _db, DateTime startDate, IMapper mapper)
        {
            if (device != null )
            {

                ReportViewModel report = new ReportViewModel();
                report.Device = device;
                //temp
                report.MinTemp = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.Temperature == _db.Measurements.Where(d=>d.Device.Name.Equals(device.Name)).Min(y => y.Temperature) && x.Device.Name.Equals(device.Name)));

                report.AvgTemp = (float?)await _db.Measurements.Where(d => d.Date >= startDate && d.Device.Name.Equals(device.Name)).AverageAsync(y => y.Temperature);
                
                report.MaxTemp = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.Temperature == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name)).Max(y => y.Temperature) && x.Device.Name.Equals(device.Name)));
                ////atmospheric pressure
                report.MinAtmosphericPressure = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.AtmosphericPressure == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name)).Min(y => y.AtmosphericPressure) && x.Device.Name.Equals(device.Name)));

                report.AvgAtmosphericPressure = (float?)await _db.Measurements.Where(d => d.Date >= startDate && d.Device.Name.Equals(device.Name)).AverageAsync(y => y.AtmosphericPressure);
                report.MaxAtmosphericPressure = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.AtmosphericPressure == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name)).Max(y => y.AtmosphericPressure) && x.Device.Name.Equals(device.Name)));

                //air humidity

                report.MinAirHumidity = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.AirHumidity == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name)).Min(y => y.AirHumidity) && x.Device.Name.Equals(device.Name)));
                report.AvgAirHumidity = (float?)await _db.Measurements.Where(d => d.Date >= startDate && d.Device.Name.Equals(device.Name)).AverageAsync(y => y.AirHumidity);

                report.MaxAirHumidity = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.AirHumidity == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name)).Max(y => y.AirHumidity) && x.Device.Name.Equals(device.Name)));
                ////light level
                report.MinLightLevel = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.LightLevel == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name))
                            .Min(y => y.LightLevel) && x.Device.Name.Equals(device.Name)));
                report.AvgLightLevel = (float?)await _db.Measurements.Where(d => d.Date >= startDate && d.Device.Name.Equals(device.Name)).AverageAsync(y => y.LightLevel);
                report.MaxLightLevel = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.LightLevel == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name))
                            .Max(y => y.LightLevel) && x.Device.Name.Equals(device.Name)));
                ////smoke level

                report.MinSmokeLevel = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.SmokeLevel == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name))
                            .Min(y => y.SmokeLevel) && x.Device.Name.Equals(device.Name)));
                report.AvgSmokeLevel = (float?)await _db.Measurements.Where(d => d.Date >= startDate && d.Device.Name.Equals(device.Name)).AverageAsync(y => y.SmokeLevel);
                report.MaxSmokeLevel = mapper.Map<Measurement, MeasurementViewModel>(
                    await _db.Measurements.FirstOrDefaultAsync(x =>
                        x.SmokeLevel == _db.Measurements.Where(d => d.Device.Name.Equals(device.Name))
                            .Max(y => y.SmokeLevel) && x.Device.Name.Equals(device.Name)));
                return report;
            }
            else
            {
                return null;
            }
        }

    }
}
