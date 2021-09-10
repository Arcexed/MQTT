﻿using Models.Models;
using Models.DBO;
namespace Models.Profile
{
    public class UserProfile: AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Ip, opt => opt.MapFrom(src => src.Ip))
                .ForMember(dest => dest.IsBlock, opt => opt.MapFrom(src => src.IsBlock))
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.IdRoleNavigation))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices))
                .ReverseMap();
        }
    }
}
