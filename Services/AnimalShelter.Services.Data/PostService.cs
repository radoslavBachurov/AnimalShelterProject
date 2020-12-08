namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private ImageBuilder imageBuilder;

        public PostService(IDeletableEntityRepository<PetPost> petPostsRepository)
        {
            this.petPostsRepository = petPostsRepository;
            this.imageBuilder = new ImageBuilder();
        }

        public async Task<int> CreatePostAsync<T>(T input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images)
        {
            var webRootPath = $"/UserImages/{categoryName}/{user.Nickname}/";
            var directory = webRoot + webRootPath;

            Directory.CreateDirectory(directory);

            var newPost = AutoMapperConfig.MapperInstance.Map<PetPost>(input);
            newPost.UserId = user.Id;

            if (newPost.PetStatus == 0)
            {
                newPost.PetStatus = PetStatus.ForAdoption;
            }

            var pictures = await this.imageBuilder.CreatePicturesAsync(images, webRootPath, user.Id, directory);
            pictures.ForEach(x => { newPost.PostPictures.Add(x); });

            await this.petPostsRepository.AddAsync(newPost);
            await this.petPostsRepository.SaveChangesAsync();

            return newPost.Id;
        }
    }
}
