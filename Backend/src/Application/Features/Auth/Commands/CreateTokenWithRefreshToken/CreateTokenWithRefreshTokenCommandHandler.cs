using Application.Common.Exceptions;
using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateTokenWithRefreshToken
{
    public class CreateTokenWithRefreshTokenCommandHandler : IRequestHandler<CreateTokenWithRefreshTokenCommand, CreatedTokenWithRefreshToken>
    {
        private IUserRepository _repository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IHttpContextAccessor _accessor;
        private ITokenHelper _tokenHelper;

        public CreateTokenWithRefreshTokenCommandHandler(IUserRepository repository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
            _accessor = accessor;
        }

        public async Task<CreatedTokenWithRefreshToken> Handle(CreateTokenWithRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var refreshTokenData = await _refreshTokenRepository.GetAsync(p => p.Client == request.Client && p.RefreshTokenValue == request.RefreshToken && p.IpAddress == remoteIpAddress, include: m => m.Include(c => c.User).ThenInclude(c=>c.UserOperationClaims).ThenInclude(x=>x.OperationClaim));

             AuthBusinessRules.ThereIsData(refreshTokenData);

            AuthBusinessRules.MatchIpAddress(refreshTokenData.IpAddress, remoteIpAddress);

           var createdToken =  _tokenHelper.CreateRefreshTokenWithStillClient(refreshTokenData.User, remoteIpAddress,refreshTokenData.Client);


            await _refreshTokenRepository.DeleteAsync(refreshTokenData);
            await _refreshTokenRepository.AddAsync(createdToken);

            return new CreatedTokenWithRefreshToken
            {
                Client = createdToken.Client,
                RefreshTokenValue = createdToken.RefreshTokenValue,
                Token = createdToken.Token,
                TokenExpiration = createdToken.TokenExpiration
                
            };
        }
    }
}
