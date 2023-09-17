using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface IImageService
{
    public Task<string> UploadPhoto(IFormFile file);
    public Task DeleteAsync(string imageUrl);
}