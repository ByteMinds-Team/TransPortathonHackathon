using Domain;

namespace Application.Repositories.EntityFramework;

public interface IMessageRepository : IRepository<Message>, IAsyncRepository<Message>
{
    Task<List<ChatDto>> GetAllChats(int userId);
}