using Application.Common.Exceptions;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Employees.Commands.UpdateEployee;

public class UpdateEployeeHandler : IRequestHandler<UpdateEployeeCommand,UpdatedEmployeeDto>
{
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEployeeHandler(IMapper mapper, IEmployeeRepository employeeRepository, IImageService imageService)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
        _imageService = imageService;
    }

    public async Task<UpdatedEmployeeDto> Handle(UpdateEployeeCommand request, CancellationToken cancellationToken)
    {
        string imagePath;
        imagePath = await _imageService.UploadPhoto(request.ProfilePhoto);
        // var mappedEmployeesEntity = _mapper.Map<Employee>(request);
        var checkOwner = await _employeeRepository.GetAsync(p=>p.Id == request.EmployeeId && p.CorporateCustomerId == request.CorporateCustomerId);
        

        EmployeeBusinessRules.checkNullData(checkOwner);
        imagePath = request.ProfilePhoto == null ? checkOwner.ProfileImagePath : await _imageService.UploadPhoto(request.ProfilePhoto);
        checkOwner.ProfileImagePath =imagePath;
        var updatedData = await _employeeRepository.UpdateAsync( _mapper.Map(request, checkOwner));
        
       var mappedDto = _mapper.Map<UpdatedEmployeeDto>(updatedData);
       return mappedDto;
    }
}