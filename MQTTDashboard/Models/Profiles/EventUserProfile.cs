using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MQTTDashboard.Models.DbModels;
using MQTTWebApi.Models;

namespace MQTTDashboard.Models.Profiles
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
