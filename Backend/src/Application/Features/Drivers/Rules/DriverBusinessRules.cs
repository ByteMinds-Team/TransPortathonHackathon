using Application.Common.Exceptions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Rules
{
    public static class DriverBusinessRules
    {
        public static void CheckIfNull(Driver driver) {
            if (driver is null)
                throw new BusinessException("Driver is not found");
        }

        public static void MultiCheckIfNull(IList<Driver> driver)
        {
            if (driver is null)
                throw new BusinessException("Driver is not found");
        }
    }
}
