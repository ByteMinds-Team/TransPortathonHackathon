using Application.Features.Auth.Models;
using Domain.Entities;

namespace Application.Services;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
    RefreshToken CreateRefreshTokenWithStillClient(User user, string ipAddress, string clientId);
}