#region

using AutoMapper;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

#endregion

namespace MQTT.Shared.Profiles
{
    public class EventUserProfile : Profile
    {
        public EventUserProfile()
        {
            CreateMap<EventsDevice, EventDeviceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ReverseMap();
        }
    }
}