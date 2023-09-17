namespace Application.Features.Offers.Dtos;

public class UpdatedOfferDto
{
    public int OfferId { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsActived { get; set; }
}