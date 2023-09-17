using Application.Common.Exceptions;
using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Models;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.LoginUserCommand;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginedUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private IMapper _mapper;
    private ITokenHelper _tokenHelper;
    private IHttpContextAccessor _accessor;


    public LoginUserCommandHandler(IUserRepository repository,
        ITokenHelper tokenHelper, IMapper mapper, IHttpContextAccessor accessor, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = repository;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _accessor = accessor;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<LoginedUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        var user = await _userRepository.GetAsync(
            u => u.Email.ToLower() == request.Email.ToLower(),
            include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));

        if (user == null)
            throw new BusinessException("This user doesn't exist");

        //var operationClaims = user.UserOperationClaims.Select(x => x.OperationClaim).ToList();

        var result = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

        if (!result)
            throw new BusinessException("Wrong credentials");

        var token = _tokenHelper.CreateRefreshToken(user, remoteIpAddress);
        await _refreshTokenRepository.AddAsync(token);
        LoginedUserDto loginedUserDto = _mapper.Map<LoginedUserDto>(token);
        return loginedUserDto;
    }
}