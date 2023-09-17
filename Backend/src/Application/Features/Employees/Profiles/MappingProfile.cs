using Application.Features.Drivers.Dtos;
using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.UpdateEployee;
using Application.Features.Employees.Dtos;
using AutoMapper;
using Domain;

namespace Application.Features.Employees.Profiles;

public class MappingProfile:Profile{
    public MappingProfile()
    {
        CreateMap<Employee,CreatedEmployeeDto>().ReverseMap();
        CreateMap<Employee,CreateEmployeeCommand>().ReverseMap();
        CreateMap<Employee,DeletedEmployeeDto>().ReverseMap();
        CreateMap<Employee,UpdatedEmployeeDto>().ReverseMap();
        CreateMap<Employee,UpdateEployeeCommand>().ReverseMap();
        //CreateMap<GetedAllEmployee,Employee>().ReverseMap().ForMember(x => x.VehicleId, opt => opt.MapFrom(p => p.Drivers.Select(x => x.Vehicle.Id)));
        CreateMap<GetedAllEmployeeByCorporateUserId, Employee>().ReverseMap();
        CreateMap<Employee, GetedAllDriverByCorporateCustomerIdDto>().ReverseMap();
        CreateMap<Employee, GetEmployeeByIdDto>().ReverseMap();
        
        
    }
}