using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reviews.Dtos
{
    public class CreatedReviewDto
    {
        public int Point { get; set; }
        public string Comment { get; set; }
        public int CorporateCustomerId { get; set; }
        public int UserId { get; set; }
    }
}
