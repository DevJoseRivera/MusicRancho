using AutoMapper;
using MusicRancho_RanchoAPI.Models;
using MusicRancho_RanchoAPI.Models.Dto;

namespace MusicRancho_RanchoAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Rancho, RanchoDTO>();
            CreateMap<RanchoDTO,Rancho>();

            CreateMap<Rancho, RanchoCreateDTO>().ReverseMap();
            CreateMap<Rancho, RanchoUpdateDTO>().ReverseMap();


            CreateMap<RanchoNumber, RanchoNumberDTO>().ReverseMap();
            CreateMap<RanchoNumber, RanchoNumberCreateDTO>().ReverseMap();
            CreateMap<RanchoNumber, RanchoNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
