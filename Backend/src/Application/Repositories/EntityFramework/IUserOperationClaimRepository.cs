﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.EntityFramework
{
    public interface IUserOperationClaimRepository : IRepository<UserOperationClaim>,IAsyncRepository<UserOperationClaim>
    {
    }
}
