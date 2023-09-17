using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateCorporateUser;

public class CreateCorporateUserCommandHandler : IRequestHandler<CreateCorporateUserCommand,CreatedCorporateUserDto>
{
    private readonly ICorporateUserRepository _corporateUser;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IImageService _imageService;

    public CreateCorporateUserCommandHandler(ICorporateUserRepository corporateUser, IHttpContextAccessor accessor, IUserOperationClaimRepository userOperationClaimRepository, IImageService imageService)
    {
        _corporateUser = corporateUser;
        _accessor = accessor;
        _userOperationClaimRepository = userOperationClaimRepository;
        _imageService = imageService;
    }

    public async Task<CreatedCorporateUserDto> Handle(CreateCorporateUserCommand request, CancellationToken cancellationToken)
    {
        
        var findedData = await _corporateUser.GetAsync(p => p.Email == request.Email);

        var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        AuthBusinessRules.BeUniqueEmail(findedData);
        HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        string imagePath = await _imageService.UploadPhoto(request.ProfilePhoto);
        CorporateCustomer user = new()
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
            CompanyName = request.CompanyName,
            ProfileImagePath = imagePath
        };

        var createdUser = await _corporateUser.AddAsync(user);

        await _userOperationClaimRepository.AddAsync(new() { OperationClaimId = 1,UserId = createdUser.Id});
        return new() {IsSuccess = true };
    }
}
