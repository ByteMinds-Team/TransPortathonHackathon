using Application.Repositories.EntityFramework;
using Domain;

namespace Application;

public interface ITransportTypeRepository : IRepository<TransportType>, IAsyncRepository<TransportType>
{

}
