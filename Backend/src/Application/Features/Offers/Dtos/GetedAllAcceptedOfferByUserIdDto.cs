using Domain;

namespace Application.Features.Offers.Dtos;

public class GetedAllAcceptedOfferByUserIdDto
{
    /* public string CompanyName { get; set; }
    public double Price { get; set; }
    public DateTime AppointmentDate { get; set; } */
    public int Id { get; set; }
    public double Price { get; set; }
    public string CompanyName { get; set; }
    public string CompanyImagePath { get; set; }
    public string CorporateUserFullName { get; set; }
    public string CorporateCustomerProfilePhotoPath { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int CorporateCustomerId { get; set; }
    public int TransportRequestId { get; set; }
    public int CrewId { get; set; }
    public bool IsAccepted { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    public List<Employee> Employees { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    //public Crew Crew { get; set; }
    public Domain.TransportRequest TransportRequest { get; set; }
}