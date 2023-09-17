using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Messages;

public class GetAllMessageQuery : IRequest<List<MessageListDto>>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OpponentUserId { get; set; }

    public string[] Roles => Array.Empty<string>();

    public class GetAllMessageQueryHandler : IRequestHandler<GetAllMessageQuery, List<MessageListDto>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetAllMessageQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<MessageListDto>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetAllAsync(
                m => (m.SenderId == request.UserId && m.ReceiverId == request.OpponentUserId)
                || (m.ReceiverId == request.UserId && m.SenderId == request.OpponentUserId),
                include: x => x.Include(message => message.Sender)
                    .Include(message => message.Receiver));

            return messages.Select(message => new MessageListDto()
            {
                Id = message.Id,
                CreatedDate = message.CreatedDate,
                ReceiverId = message.ReceiverId,
                SenderId = message.SenderId,
                SenderImagePath = message.Sender.ProfileImagePath,
                SenderName = message.Sender.FullName,
                Text = message.Text
            })
            .ToList();
        }
    }
}