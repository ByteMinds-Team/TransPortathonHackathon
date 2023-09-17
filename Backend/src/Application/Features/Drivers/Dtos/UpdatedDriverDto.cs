using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Dtos
{
    public class UpdatedDriverDto
    {
        public string FullName { get; set; }
        public int CorporateuserId { get; set; }
        public int VehicleId { get; set; }
    }
}
