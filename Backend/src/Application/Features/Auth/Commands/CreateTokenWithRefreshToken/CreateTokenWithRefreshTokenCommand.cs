using Application.Features.Auth.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateTokenWithRefreshToken
{
    public class CreateTokenWithRefreshTokenCommand:IRequest<CreatedTokenWithRefreshToken>
    {
        //public string UserId { get; set; }
        //public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Client { get; set; }
    }
}
