using Application.Common.Exceptions;
using Domain;

namespace Application.Features.Crews.Rules;

public static class CrewBusinessRules
{
    public static void checkEmployeeNullData(IList<Employee> employees)
    {
        if (employees is null)
        {
            throw new BusinessException("crew is not found");
        }
    }
    
    public static void checkNullSingleData(Crew employees)
    {
        if (employees is null)
        {
            throw new BusinessException("crew is not found");
        }
    }
}