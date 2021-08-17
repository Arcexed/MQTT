using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
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
                .ForMember(dest =>dest.CreateDate,opt => opt.MapFrom(src=> src.CreateDate))
                .ForMember(dest => dest.Descr, opt=> opt.MapFrom(src => src.Descr))
                .ForMember(dest=> dest.EditDate, opt=>opt.MapFrom(src=>src.EditDate))
                .ForMember(dest => dest.Geo,opt=> opt.MapFrom(src=>src.Geo))
                .ReverseMap();
        }
    }
}
