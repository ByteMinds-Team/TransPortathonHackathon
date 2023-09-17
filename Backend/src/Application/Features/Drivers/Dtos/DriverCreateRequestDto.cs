using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Dtos
{
    public class DriverCreateRequestDto
    {
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
    }
}
