using Application.Common.Behaviours.Authorization;
using Application.Features.Messages;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class GetAllChatQuery : IRequest<List<ChatDto>>, ISecuredRequest
{
    public int UserId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class GetAllChatQueryHandler : IRequestHandler<GetAllChatQuery, List<ChatDto>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetAllChatQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<ChatDto>> Handle(GetAllChatQuery request, CancellationToken cancellationToken)
        {
            var result = await _messageRepository.GetAllChats(request.UserId);
            return result;

        }
    }
}
