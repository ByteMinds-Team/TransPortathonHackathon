using Application.Common.Behaviours.Authorization;
using Application.Features.Auth.Models;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries.GetListUser;

public class GetUserListQuery : IRequest<UserListModel>,ISecuredRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public string[] Roles => new string[] {"admin"};

    public class GetUsersListQuertHandler : IRequestHandler<GetUserListQuery, UserListModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersListQuertHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserListModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetListAsync(
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(c => c.OperationClaim),
                index: request.Page,
                size: request.PageSize);

            var userListModel = _mapper.Map<UserListModel>(result);

            return userListModel;
        }
    }
}