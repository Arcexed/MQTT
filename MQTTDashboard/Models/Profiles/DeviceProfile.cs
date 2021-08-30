using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MQTTDashboard.Models.DbModels;
using MQTTWebApi.Models.ForReport;

namespace MQTTWebApi.Models
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest =>dest.CreateDate,opt => opt.MapFrom(src=> src.CreatingDate))
                .ForMember(dest => dest.Descr, opt=> opt.MapFrom(src => src.Descr))
                .ForMember(dest=> dest.EditDate, opt=>opt.MapFrom(src=>src.EditingDate))
                .ForMember(dest => dest.Geo,opt=> opt.MapFrom(src=>src.Geo))
                .ForMember(dest => dest.LastTenMeasurements,opt=>opt.MapFrom(src => src.Measurements.OrderByDescending(d => d.Date).Take(10)))
                .ForMember(dest => dest.LastTenEvents, opt =>opt.MapFrom(scr=>scr.EventsDevice.OrderByDescending(d => d.Date).Take(10)))
                .ForMember(dest => dest.User,opt=> opt.MapFrom(src=>src.User))

                .ReverseMap();
        }
    }
}
