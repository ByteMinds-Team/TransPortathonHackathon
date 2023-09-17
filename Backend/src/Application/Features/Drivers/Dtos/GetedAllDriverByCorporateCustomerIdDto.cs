using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Dtos
{
    public class GetedAllDriverByCorporateCustomerIdDto
    {
        public string FullName { get; set; }
        public string ProfileImagePath { get; set; }
        public string NumberPlate { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int ModelYear { get; set; }
        public string VehicleImagePath { get; set; }
    }
}
