using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MQTTDashboard.Models.DbModels;

namespace MQTTWebApi.Models.Profiles
{
    public class EventDeviceProfile : Profile
    {
        public EventDeviceProfile()
        {
            CreateMap<EventsDevice, EventDeviceViewModel>()
                .ForMember(dest =>dest.Id,opt=>opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date,opt=>opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Message,opt=>opt.MapFrom(src=>src.Message))
                .ReverseMap();
        }
    }
}
