using AutoMapper;
using EmployeeWEBAPI.Models;
using EmployeeWEBAPI.Models.DTO;

namespace EmployeeWEBAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee,EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
        }
    }
}
