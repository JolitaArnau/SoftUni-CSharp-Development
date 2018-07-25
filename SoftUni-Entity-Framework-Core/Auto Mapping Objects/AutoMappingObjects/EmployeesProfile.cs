namespace AutoMappingObjects
{
    using AutoMapper;
    
    using Models;
    using ModelsDto;

    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>().ReverseMap();
            CreateMap<Employee, EmployeeManagerDto>().ReverseMap();
        }
    }
}