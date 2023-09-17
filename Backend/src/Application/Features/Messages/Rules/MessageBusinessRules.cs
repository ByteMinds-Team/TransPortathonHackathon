using Application.Common.Exceptions;
using Application.Features.Employees.Queries.GetAllByCorporateId;
using Application.Repositories.EntityFramework;
using Domain.Entities;

namespace Application.Features.Messages;

public class MessageBusinessRules
{
    public void UserShouldExist(User user)
    {
        if (user is null) throw new BusinessException("User does not exist");
    }
}