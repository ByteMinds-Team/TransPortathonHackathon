using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Dtos;

public class CreatedUserDto
{
    public string Client { get; set; }
    public string RefreshTokenValue { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
}
