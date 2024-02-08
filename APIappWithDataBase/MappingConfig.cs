using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;
using AutoMapper;

namespace APIappWithDataBase
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();//.ReverseMap();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDto>();//.ReverseMap();
            CreateMap<VillaNumberDto, VillaNumber>();

            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
        }
    }
}
