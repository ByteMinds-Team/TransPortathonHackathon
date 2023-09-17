using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Crews.Dtos
{
    public class GetedAllCrewByCorporateUserIdDto
    {
        public int Id { get; set; }
        public string CrewName { get; set; }
        public List<Employee> Employees { get; set; }
        
    }
}
