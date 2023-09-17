using Application.Features.Auth.Dtos;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.CreateCorporateUser
{
    public class CreateCorporateUserCommand : IRequest<CreatedCorporateUserDto>
    {
        public string FullName { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
    }
}
