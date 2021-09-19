#region

using AutoMapper;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

#endregion

namespace MQTT.Shared.Profiles
{
    public class MeasurementProfile : Profile
    {
        public MeasurementProfile()
        {
            CreateMap<Measurement, MeasurementViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.AirHumidity, opt => opt.MapFrom(src => src.AirHumidity))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.LightLevel, opt => opt.MapFrom(src => src.LightLevel))
                .ForMember(dest => dest.AtmosphericPressure, opt => opt.MapFrom(src => src.AtmosphericPressure))
                .ForMember(dest => dest.SmokeLevel, opt => opt.MapFrom(src => src.SmokeLevel))
                .ForMember(dest => dest.RadiationLevel, opt => opt.MapFrom(src => src.RadiationLevel))
                .ReverseMap();
        }
    }
}