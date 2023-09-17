using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Messages;

public class CreateMessageCommand : IRequest<CreatedMessageDto>, ISecuredRequest
{
    public string Text { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreatedMessageDto>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly MessageBusinessRules _messageBusinessRules;
        private readonly IUserRepository _userRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper, MessageBusinessRules messageBusinessRules, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _messageBusinessRules = messageBusinessRules;
            _userRepository = userRepository;
        }

        public async Task<CreatedMessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetAsync(x => x.Id == request.SenderId);
            var receiver = await _userRepository.GetAsync(x => x.Id == request.ReceiverId);
            _messageBusinessRules.UserShouldExist(sender);
            _messageBusinessRules.UserShouldExist(receiver);
            var message = _mapper.Map<Message>(request);
            var createdMessage = await _messageRepository.AddAsync(message);
            return new()
            {
                Id = createdMessage.Id,
                Text = createdMessage.Text,
                CreatedDate = createdMessage.CreatedDate,
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
                SenderName = sender.FullName,
                SenderImagePath = sender.ProfileImagePath
            };
        }
    }
}