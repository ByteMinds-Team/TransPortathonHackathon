using Application.Common.Exceptions;
using Domain;

namespace Application.Features.Employees.Rules;

public static class EmployeeBusinessRules
{
    public static void checkNullData(Employee employee)
    {
        if (employee is null)
        {
            throw new BusinessException("employee is not found");
        }
    }
}