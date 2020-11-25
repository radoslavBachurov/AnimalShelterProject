namespace AnimalShelter.Web.Infrastructure
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;

    public class ImageBuilder
    {
        public async Task<string> SaveImage(IFormFile image, string directory, string pictureId)
        {
            using (var stream = image.OpenReadStream())

            using (var pic = Image.Load(stream, out IImageFormat format))

            using (FileStream fs = new FileStream(directory + pictureId + $".{format.Name.ToLower()}", FileMode.Create))
            {
                await image.CopyToAsync(fs);

                return format.Name.ToLower();
            }
        }
    }
}
