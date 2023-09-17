using Application.Features.Employees.Dtos;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand,CreatedEmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper, IImageService imageService)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<CreatedEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await _imageService.UploadPhoto(request.ProfilePhoto);
        var mappedEmployeeEntity = _mapper.Map<Employee>(request);
        mappedEmployeeEntity.ProfileImagePath = imageUrl;
        var addedData = await _employeeRepository.AddAsync(mappedEmployeeEntity);
        var mappedEmployeeDto = _mapper.Map<CreatedEmployeeDto>(addedData);
        return mappedEmployeeDto;
    }
}