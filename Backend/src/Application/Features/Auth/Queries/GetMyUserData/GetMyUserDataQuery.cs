using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth;

public class GetMyUserDataQuery : IRequest<UserDto>, ISecuredRequest
{
    public int UserId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class GetMyUserDataQueryHandler : IRequestHandler<GetMyUserDataQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetMyUserDataQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetMyUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Id == request.UserId,
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim)
            );
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                ImagePath = user.ProfileImagePath,
                OperationClaims = user.UserOperationClaims.Select(u => u.OperationClaim).ToList()
            };
        }
    }
}
