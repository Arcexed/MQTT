using Models.DbModels;
using Models.DBO;
using System.Linq;

namespace Models.Profile
{
    public class DeviceProfile : AutoMapper.Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest =>dest.CreatingDate,opt => opt.MapFrom(src=> src.CreatingDate))
                .ForMember(dest => dest.Descr, opt=> opt.MapFrom(src => src.Descr))
                .ForMember(dest=> dest.EditingDate, opt=>opt.MapFrom(src=>src.EditingDate))
                .ForMember(dest => dest.Geo,opt=> opt.MapFrom(src=>src.Geo))
                .ForMember(dest => dest.LastThreeMeasurements,opt=>opt.MapFrom(src => src.Measurements.OrderByDescending(d => d.Date).Take(3)))
                .ForMember(dest => dest.LastThreeEvents, opt =>opt.MapFrom(scr=>scr.EventsDevices.OrderByDescending(d => d.Date).Take(3)))
                .ForMember(dest => dest.User,opt=> opt.MapFrom(src=>src.IdUserNavigation))
                .ForMember(dest => dest.IdUser,opt=> opt.MapFrom(src=>src.IdUser))
                .ReverseMap();
        }
    }
}
