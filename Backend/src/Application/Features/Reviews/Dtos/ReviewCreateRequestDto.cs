namespace Application.Features.Reviews.Dtos;

public class ReviewCreateRequestDto
{
    public int Point { get; set; }
    public string Comment { get; set; }
    public int OfferId { get; set; }
}