using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Dtos
{
    public class GetedAllEmployeeByCorporateUserId
    {
        public string FullName { get; set; }
        public string ProfileImagePath { get; set; }

        public int VehicleId { get; set; }
        public string ImagePath { get; set; }
    }
}
