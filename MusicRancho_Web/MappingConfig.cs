using AutoMapper;
using MusicRancho_Web.Models.Dto;

namespace MusicRancho_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RanchoDTO,RanchoCreateDTO>().ReverseMap();
            CreateMap<RanchoDTO, RanchoUpdateDTO>().ReverseMap();

            CreateMap<RanchoNumberDTO, RanchoNumberCreateDTO>().ReverseMap();
            CreateMap<RanchoNumberDTO, RanchoNumberUpdateDTO>().ReverseMap();
        }
    }
}
