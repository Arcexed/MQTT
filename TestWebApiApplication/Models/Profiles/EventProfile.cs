﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MQTTWebApi.Models.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventViewModel>()
                .ForMember(dest =>dest.Id,opt=>opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date,opt=>opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Message,opt=>opt.MapFrom(src=>src.Message))
                .ReverseMap();
        }
    }
}