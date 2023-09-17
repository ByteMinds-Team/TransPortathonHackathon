using Domain;

namespace Application.Features.TransportRequest;

public class ListTransportRequestDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateGap dateGap { get; set; }
    public Domain.TransportType TransportType { get; set; }
}