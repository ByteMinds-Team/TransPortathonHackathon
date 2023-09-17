using Application.Common.Exceptions;
using Domain;

namespace Application.Features.Vehicles.Rules;

public class VehicleBusinessRules
{
    public void VehicleShouldExist(Vehicle vehicle)
    {
        if (vehicle is null) throw new BusinessException("Vehicle not found");
    }
    public void VehicleWithTheSameNumberPlateShouldNotExist(Vehicle vehicle)
    {
        if (vehicle is not null) throw new BusinessException("Vehicle with given number plate is already exists");
    }
}