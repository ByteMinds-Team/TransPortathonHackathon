using Application.Common.Exceptions;
using Application.Repositories.EntityFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules
{
    public static class AuthBusinessRules
    {

        public static void ThereIsData(RefreshToken user)
        {
            if (user == null)
            {
                throw new BusinessException("data is not found");
            }

        }

        public static void MatchIpAddress(string oldIp, string newIp)
        {
            if (!oldIp.Equals(newIp)) throw new BusinessException("this ip address different");
        }

        public static void BeUniqueEmail(User user)
        {
            if (user != null)
            {
                throw new BusinessException("user is exist");
            }
        }
    }
}
