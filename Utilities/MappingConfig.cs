using AutoMapper;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>();
            CreateMap<VillaNumberDTO, VillaNumber>();

            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
