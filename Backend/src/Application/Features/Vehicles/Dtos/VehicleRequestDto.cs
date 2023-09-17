using Microsoft.AspNetCore.Http;

namespace Application;

public class VehicleRequestDto
{
    public string NumberPlate { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int ModelYear { get; set; }
    public IFormFile? CarImage { get; set; }
}