using Application.Common.Exceptions;

namespace Application.Features.TransportType.Rules;

public class TransportTypeBusinessRules
{
    private readonly ITransportTypeRepository _transportTypeRepository;

    public TransportTypeBusinessRules(ITransportTypeRepository transportTypeRepository)
        => (_transportTypeRepository) = (transportTypeRepository);

    public async Task TransportTypeShouldNotDuplicate(string type)
    {
        var transportType = await _transportTypeRepository.GetAsync(t => t.Type.ToLower() == type.ToLower());
        if (transportType is not null) throw new BusinessException("This type is already exists.");
    }
}