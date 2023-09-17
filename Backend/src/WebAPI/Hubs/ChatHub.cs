using Application;
using Application.Features.Messages;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI;

public class MessageSentRequest
{
    public string Text { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
}

public class ChatHub : Hub
{
    public static readonly List<Connection> Connections = new() { };

    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessageAsync(MessageSentRequest message)
    {
        var userId = Context.User.GetUserId();
        var receiverConnection = Connections.FirstOrDefault(c => c.UserId == message.ReceiverId);
        var createMessageCommand = new CreateMessageCommand(){
            ReceiverId = message.ReceiverId,
            SenderId = userId,
            Text = message.Text
        };

        var result = await _mediator.Send(createMessageCommand);

        if (receiverConnection is null) {
            await Clients.Caller.SendAsync("receiveMessage", result);
        }
        else {
            var clients = new string[2] { Context.ConnectionId, receiverConnection.ConnectionId };
            await Clients.Clients(clients).SendAsync("receiveMessage", result);
        }
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User.GetUserId();
        var result = Connections.FirstOrDefault(c => c.UserId == userId);
        if (result is not null)
        {
            result.Status = true;
            result.ConnectionId = Context.ConnectionId;
        }
        else
        {
            Connection connection = new()
            {
                UserId = Context.User.GetUserId(),
                ConnectionId = Context.ConnectionId,
                Status = true
            };
            Connections.Add(connection);
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User.GetUserId();
        var result = Connections.FirstOrDefault(c => c.UserId == userId);
        if (result is not null)
        {
            result.Status = false;
        }

        return base.OnDisconnectedAsync(exception);
    }
}
