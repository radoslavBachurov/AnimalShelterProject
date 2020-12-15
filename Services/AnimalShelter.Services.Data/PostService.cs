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

            var pictures = await this.imageBuilder.CreatePicturesAsync(images, pathInRoot, user.Id, directory);
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

        public async Task UpdateAdoptPostAsync(EditAdoptPetInputModel input, string webRoot, string categoryName, IEnumerable<IFormFile> images)
        {
            var baseModel = this.petPostsRepository.All().FirstOrDefault(x => x.Id == input.Id);
            var userInfo = this.petPostsRepository.All().Where(x => x.Id == input.Id).Select(x => new { userId = x.UserId, nickName = x.User.Nickname }).FirstOrDefault();

            if (images != null)
            {
                string pathInRoot = CreatePathInRoot(categoryName, userInfo.nickName);
                var directory = webRoot + pathInRoot;

                Directory.CreateDirectory(directory);

                var pictures = await this.imageBuilder.CreatePicturesAsync(images, pathInRoot, userInfo.userId, directory);
                pictures.ForEach(x => { x.IsCoverPicture = false; });
                pictures.ForEach(x => { baseModel.PostPictures.Add(x); });
            }

            baseModel.Name = input.Name;
            baseModel.Description = input.Description;
            baseModel.Location = input.Location;
            baseModel.Sex = input.Sex;
            baseModel.Type = input.Type;

            await this.petPostsRepository.SaveChangesAsync();
        }

        private static string CreatePathInRoot(string categoryName, string userNickname)
        {
            return $"/UserImages/{categoryName}/{userNickname}/";
        }
    }
}
