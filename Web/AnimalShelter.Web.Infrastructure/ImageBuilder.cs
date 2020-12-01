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
        private int Height { get; set; }

        private int Width { get; set; }

        private string Extention { get; set; }

        public async Task SaveImage(IFormFile image, string directory, string pictureId)
        {
            using (var stream = image.OpenReadStream())

            using (var pic = Image.Load(stream, out IImageFormat format))

            using (FileStream fs = new FileStream(directory + pictureId + $".{format.Name.ToLower()}", FileMode.Create))
            {
                await image.CopyToAsync(fs);

                this.Height = pic.Height;
                this.Width = pic.Width;
                this.Extention = format.Name.ToLower();
            }
        }

        public async Task<List<Picture>> CreatePictures(IEnumerable<IFormFile> images, string webRootPath, string userId, string directory)
        {
            List<Picture> pictures = new List<Picture>();

            int counter = 0;
            foreach (var image in images)
            {
                var picture = new Picture() { Path = webRootPath, UserId = userId };

                await this.SaveImage(image, directory, picture.Id);

                picture.Extension = this.Extention;
                picture.Width = this.Width;
                picture.Height = this.Height;

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
