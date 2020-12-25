namespace AnimalShelter.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.Processing;

    public class ImageBuilder
    {
        private int Height { get; set; }

        private int Width { get; set; }

        private string Directory { get; set; }

        public async Task<List<Picture>> CreatePicturesAsync(IEnumerable<IFormFile> images, string webRootPath, string userId, string directory, string categoryName, bool makeCoverPhoto = false)
        {
            this.Directory = directory;
            List<Picture> pictures = new List<Picture>();

            int counter = 1;
            if (makeCoverPhoto)
            {
                counter = 0;
            }

            foreach (var image in images)
            {
                var picture = new Picture();
                picture.Path = webRootPath;

                if (categoryName == "Users")
                {
                    picture.UserPictureId = userId;
                }
                else
                {
                    picture.PostPictureId = userId;
                }

                await this.SaveImageAsync(image, picture.Id);

                picture.Width = this.Width;
                picture.Height = this.Height;
                picture.Extension = ".png";

                picture.Path += picture.Id + picture.Extension;

                if (counter == 0)
                {
                    picture.IsCoverPicture = true;
                    counter++;
                }

                pictures.Add(picture);
            }

            return pictures;
        }

        private async Task SaveImageAsync(IFormFile image, string pictureId)
        {
            using (var imageStream = image.OpenReadStream())

            using (var pic = Image.Load(imageStream, out IImageFormat format))
            {
                pic.Mutate(
                              x => x.Resize(
                                  new ResizeOptions
                                  {
                                      Mode = ResizeMode.Min,
                                      Size = new Size(730, 0),
                                      Position = AnchorPositionMode.Center,
                                  }));

                this.Height = pic.Height;
                this.Width = pic.Width;

                var tempPath = this.Directory + Path.GetRandomFileName() + ".png";
                await using (var stream = File.OpenWrite(tempPath))
                {
                    pic.SaveAsPng(
                        stream,
                        new PngEncoder
                        {
                            FilterMethod = PngFilterMethod.Adaptive,
                            CompressionLevel = PngCompressionLevel.BestCompression,
                            ColorType = PngColorType.Palette,
                        });
                }

                var filePath = this.Directory + pictureId + ".png";
                File.Delete(filePath);
                File.Move(tempPath, filePath);
            }
        }
    }
}
