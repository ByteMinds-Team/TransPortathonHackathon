using Application;
using Application.Features.Offers.Queries.GetAllAcceptedOfferByUserId;
using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class MessageRepository : EfRepositoryBase<Message, BaseDbContext>, IMessageRepository
{
    private readonly BaseDbContext baseDbContext;
    public MessageRepository(BaseDbContext context, BaseDbContext baseDbContext) : base(context)
    {
        this.baseDbContext = baseDbContext;
    }

    public async Task<List<ChatDto>> GetAllChats(int userId)
    {
        var query = from m in baseDbContext.Messages
                    join u in baseDbContext.Users
                    on m.ReceiverId equals u.Id
                    where (m.SenderId == userId || m.ReceiverId == userId) && u.Id != userId
                    select new ChatDto()
                    {
                        UserId = u.Id,
                        FullName = u.FullName,
                        ProfileImagePath = u.ProfileImagePath
                    };

        var distinctQuery = query.Distinct();

        var result = distinctQuery.ToList();
        return result;
    }
}