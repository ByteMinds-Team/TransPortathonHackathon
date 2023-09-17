using Domain;

namespace Application.Repositories.EntityFramework;

public interface ITransportRequestRepository : IRepository<TransportRequest>, IAsyncRepository<TransportRequest>
{

}