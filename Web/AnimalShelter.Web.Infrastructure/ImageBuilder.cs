namespace AnimalShelter.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using AnimalShelter.Data.Models;
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

        public async Task<List<Picture>> CreatePictures(IEnumerable<IFormFile> images, string webRootPath, string userId, string directory)
        {
            List<Picture> pictures = new List<Picture>();

            int counter = 0;
            foreach (var image in images)
            {
                var picture = new Picture() { Path = webRootPath, UserId = userId };

                string extension = await this.SaveImage(image, directory, picture.Id);

                picture.Extension = extension;

                picture.Path += picture.Id + "." + picture.Extension;

                if (counter == 0)
                {
                    picture.IsCoverPicture = true;
                    counter++;
                }

                pictures.Add(picture);
            }

            return pictures;
        }
    }
}
