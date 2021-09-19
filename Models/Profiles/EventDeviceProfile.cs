﻿using AutoMapper;
using Models.Models;
using Models.DBO;

namespace Models.Profile
{
    public class EventDeviceProfile : AutoMapper.Profile
    {
        public EventDeviceProfile()
        {
            CreateMap<EventsDevice, EventDeviceViewModel>()
                .ForMember(dest =>dest.Id,opt=>opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date,opt=>opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Message,opt=>opt.MapFrom(src=>src.Message))
                .ForMember(dest => dest.IdDevice,opt => opt.MapFrom(src=>src.DeviceId))
                .ForMember(dest => dest.Device,opt => opt.MapFrom(src=>src.Device))
                .ReverseMap();
        }
    }
}