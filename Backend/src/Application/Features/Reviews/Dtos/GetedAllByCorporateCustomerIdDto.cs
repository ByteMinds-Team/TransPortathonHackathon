using Domain.Entities;

namespace Application.Features.Reviews.Dtos;

public class GetedAllByCorporateCustomerIdDto
{
    public int Point { get; set; }
    public string Comment { get; set; }
    public int CorporateCustomerId { get; set; }
    public string FullName { get; set; }
    public string ProfileImagePath { get; set; }
}