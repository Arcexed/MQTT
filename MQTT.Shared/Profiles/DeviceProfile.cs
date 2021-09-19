using System.Linq;
using AutoMapper;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

namespace MQTT.Shared.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatingDate, opt => opt.MapFrom(src => src.CreatingDate))
                .ForMember(dest => dest.Desc, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.EditingDate, opt => opt.MapFrom(src => src.EditingDate))
                .ForMember(dest => dest.Geo, opt => opt.MapFrom(src => src.Geo))
                .ForMember(dest => dest.LastThreeMeasurements,
                    opt => opt.MapFrom(src => src.Measurements.OrderByDescending(d => d.Date).Take(3)))
                .ForMember(dest => dest.LastThreeEvents,
                    opt => opt.MapFrom(scr => scr.EventsDevices.OrderByDescending(d => d.Date).Take(3)))
                .ReverseMap();
        }
    }
}