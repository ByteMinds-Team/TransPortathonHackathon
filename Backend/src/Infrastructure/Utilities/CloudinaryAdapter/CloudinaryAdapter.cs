using Application.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Utilities.CloudinaryAdapter;

public class CloudinaryAdapter : IImageService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryAdapter(IConfiguration configuration)
    {
        Account? account = configuration.GetSection("Cloudinary").Get<Account>();
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadPhoto(IFormFile file)
    {
        
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream: file.OpenReadStream()),
            Folder = "TransportHackathonPics",
            UseFilename = false,
            UniqueFilename = true,
            Overwrite = false,
            Transformation = new Transformation().Width(1280).Height(720).Crop("fill")
        };
        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.Url.ToString();
    }
    
    public async Task DeleteAsync(string imageUrl)
    {
        DeletionParams deletionParams = new(GetPublicId(imageUrl));
        await _cloudinary.DestroyAsync(deletionParams);
    }

    private string GetPublicId(string imageUrl)
    {
        int startIndex = imageUrl.LastIndexOf('/') + 1;
        int endIndex = imageUrl.LastIndexOf('.');
        int length = endIndex - startIndex;
        return imageUrl.Substring(startIndex, length);
    }
}