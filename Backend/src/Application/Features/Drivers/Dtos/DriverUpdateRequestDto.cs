using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Dtos
{
    public class DriverUpdateRequestDto
    {
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
    }
}
