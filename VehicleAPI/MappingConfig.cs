using AutoMapper;
using VehicleAPI.Models;
using VehicleAPI.Models.DTO;

namespace VehicleAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();
            CreateMap<Vehicle, UpdateVehicleDto>().ReverseMap();
        }
    }
}
