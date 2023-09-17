using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;

namespace Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,DeletedEmployeeDto>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<DeletedEmployeeDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var gettedData =await _employeeRepository.GetAsync(p=>p.Id == request.EmployeeId && p.CorporateCustomerId == request.CorporateCustomerId);
        EmployeeBusinessRules.checkNullData(gettedData);
        var deletedData =await  _employeeRepository.DeleteAsync(gettedData);
        var mappedData =  _mapper.Map<DeletedEmployeeDto>(deletedData);
        return mappedData;
    }
}