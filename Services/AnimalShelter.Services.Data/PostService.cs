namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.PostModels;
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
            var pathInRoot = CreatePathInRoot(categoryName, user.Nickname);
            var directory = webRoot + pathInRoot;

            Directory.CreateDirectory(directory);

            var newPost = AutoMapperConfig.MapperInstance.Map<PetPost>(input);
            newPost.UserId = user.Id;

            if (newPost.PetStatus == 0)
            {
                newPost.PetStatus = PetStatus.ForAdoption;
            }

            var pictures = await this.imageBuilder.CreatePicturesAsync(images, pathInRoot, user.Id, directory, categoryName, true);
            pictures.ForEach(x => { newPost.PostPictures.Add(x); });

            await this.petPostsRepository.AddAsync(newPost);
            await this.petPostsRepository.SaveChangesAsync();

            return newPost.Id;
        }

        public T GetPostById<T>(int id)
        {
            var model = this.petPostsRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return model;
        }

        public async Task ChangeStatusAsync(int id)
        {
            var postToChange = this.petPostsRepository.All().FirstOrDefault(x => x.Id == id);

            if (postToChange.PetStatus == PetStatus.ForAdoption)
            {
                postToChange.PetStatus = PetStatus.Adopted;
            }
            else
            {
                postToChange.PetStatus = PetStatus.LostFoundBackInHome;
            }

            await this.petPostsRepository.SaveChangesAsync();
        }

        public async Task UpdatePetPostAsync<T>(T input, string webRoot, string categoryName, IEnumerable<IFormFile> images, int id)
        {
            var baseModel = this.petPostsRepository.All().FirstOrDefault(x => x.Id == id);
            var userInfo = this.petPostsRepository.All().Where(x => x.Id == id).Select(x => new { userId = x.UserId, nickName = x.User.Nickname }).FirstOrDefault();

            if (images != null)
            {
                string pathInRoot = CreatePathInRoot(categoryName, userInfo.nickName);
                var directory = webRoot + pathInRoot;

                Directory.CreateDirectory(directory);

                var pictures = await this.imageBuilder.CreatePicturesAsync(images, pathInRoot, userInfo.userId, directory, categoryName);
                pictures.ForEach(x => { baseModel.PostPictures.Add(x); });
            }

            var newModel = AutoMapperConfig.MapperInstance.Map<PetPost>(input);
            baseModel.Description = newModel.Description;
            baseModel.Location = newModel.Location;
            baseModel.Sex = newModel.Sex;
            baseModel.Type = newModel.Type;
            baseModel.Name = newModel.Name;

            if (newModel.PetStatus != 0)
            {
                baseModel.PetStatus = newModel.PetStatus;
            }

            await this.petPostsRepository.SaveChangesAsync();
        }

        private static string CreatePathInRoot(string categoryName, string userId)
        {
            return $"/UserImages/{categoryName}/{userId}/";
        }
    }
}
