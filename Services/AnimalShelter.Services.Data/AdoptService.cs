using AnimalShelter.Data.Common.Repositories;
using AnimalShelter.Data.Models;
using AnimalShelter.Web.ViewModels.Adopt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using Microsoft.AspNetCore.Http;

namespace AnimalShelter.Services.Data
{
    public class AdoptService : IAdoptService
    {
        private readonly IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository;

        public AdoptService(IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository)
        {
            this.adoptionPostsRepository = adoptionPostsRepository;
        }

        public async Task CreateAdoptionPost(CreatePetInputModel input, ApplicationUser user, string webRoot)
        {
            var directory = webRoot + $"/UserImages/Adopt/{user.Nickname}/";

            Directory.CreateDirectory(directory);

            var newAdoptPost = new PetAdoptionPost()
            {
                Description = input.Description,
                Location = input.Location,
                Sex = input.Sex,
                Type = input.Type,
                UserId = user.Id,
            };

            foreach (var image in input.Images)
            {
                var picture = new Picture() { Path = directory, UserId = user.Id };
                newAdoptPost.PostPictures.Add(picture);

                using (var stream = image.OpenReadStream())

                using (var pic = Image.Load(stream, out IImageFormat format))

                using (FileStream fs = new FileStream(directory + picture.Id + $".{format.Name.ToLower()}", FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }
            }

            await this.adoptionPostsRepository.AddAsync(newAdoptPost);
            await this.adoptionPostsRepository.SaveChangesAsync();
        }
    }
}
