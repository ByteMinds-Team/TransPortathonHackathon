namespace Application.Features.Offers.Dtos;

public class CreatedOfferDto
{
    public double Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int CorporateCustomerId { get; set; }
    public int ShippingRequestId { get; set; }
    public int CrewId { get; set; }
}