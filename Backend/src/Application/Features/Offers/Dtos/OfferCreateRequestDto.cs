namespace Application.Features.Offers.Dtos;

public class OfferCreateRequestDto
{
    public double Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int TransportRequestId { get; set; }
    public int VehicleId { get; set; }
    public int CrewId { get; set; }
}