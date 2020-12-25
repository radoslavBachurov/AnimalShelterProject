namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.Pet;
    using AnimalShelter.Web.ViewModels.StoriesModels;
    using Microsoft.AspNetCore.Http;

    public class HappyStoriesService : IHappyStoriesService
    {
        private readonly IDeletableEntityRepository<SuccessStory> storyRepository;
        private readonly IDeletableEntityRepository<UserSuccessStoryLikes> userSuccessStoryLikes;
        private ImageBuilder imageBuilder;

        public HappyStoriesService(
            IDeletableEntityRepository<SuccessStory> storyRepository,
            IDeletableEntityRepository<UserSuccessStoryLikes> userSuccessStoryLikes)
        {
            this.imageBuilder = new ImageBuilder();
            this.storyRepository = storyRepository;
            this.userSuccessStoryLikes = userSuccessStoryLikes;
        }

        public async Task<int> CreateStoryAsync<T>(T input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images)
        {
            var pathInRoot = CreatePathInRoot(categoryName, user.Nickname);
            var directory = webRoot + pathInRoot;

            Directory.CreateDirectory(directory);

            var newStory = AutoMapperConfig.MapperInstance.Map<SuccessStory>(input);
            newStory.UserId = user.Id;

            var pictures = await this.imageBuilder.CreatePicturesAsync(images, pathInRoot, user.Id, directory, categoryName, true);
            pictures.ForEach(x => { newStory.PostPictures.Add(x); });

            await this.storyRepository.AddAsync(newStory);
            await this.storyRepository.SaveChangesAsync();

            return newStory.Id;
        }

        public async Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input, ApplicationUser user)
        {
            var outputModel = new LikeOutputModel();
            var post = this.storyRepository.All().Where(x => x.Id == input.PostId && x.IsApproved).FirstOrDefault();

            if (post != null)
            {
                if (input.IsLiked)
                {
                    post.Likes--;
                    var like = this.userSuccessStoryLikes.AllAsNoTracking().Where(x => x.ApplicationUserId == user.Id && x.SuccessStoryId == input.PostId).FirstOrDefault();
                    this.userSuccessStoryLikes.HardDelete(like);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = false;

                    await this.storyRepository.SaveChangesAsync();
                    await this.userSuccessStoryLikes.SaveChangesAsync();

                    return outputModel;
                }
                else
                {
                    var userLikes = new UserSuccessStoryLikes() { SuccessStoryId = post.Id, ApplicationUserId = user.Id };
                    post.Likes++;
                    await this.userSuccessStoryLikes.AddAsync(userLikes);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = true;

                    await this.storyRepository.SaveChangesAsync();
                    await this.userSuccessStoryLikes.SaveChangesAsync();

                    return outputModel;
                }
            }

            outputModel.Likes = -1;
            return outputModel;
        }

        public StoryProfileViewModel GetStoryProfile(int postId, ApplicationUser user)
        {
            var userLikedThisStory = new List<UserSuccessStoryLikes>();
            if (user != null)
            {
                userLikedThisStory = this.userSuccessStoryLikes.AllAsNoTracking()
               .Where(x => x.ApplicationUserId == user.Id && x.SuccessStoryId == postId).ToList();
            }

            var viewModel = this.storyRepository.AllAsNoTracking()
                         .Where(x => x.Id == postId && x.IsApproved)
                         .To<StoryProfileViewModel>()
                         .FirstOrDefault();

            var postCreatorId = this.storyRepository.AllAsNoTracking()
                          .Where(x => x.Id == postId && x.IsApproved == true).FirstOrDefault().UserId;

            var currentUserId = user?.Id;
            viewModel.IsPostLiked = userLikedThisStory.Any();
            viewModel.IsPostCreator = currentUserId == postCreatorId;

            return viewModel;
        }

        public async Task DeleteStoryAsync(int storyId, string userId)
        {
            var story = this.storyRepository.All().Where(x => x.Id == storyId).FirstOrDefault();

            if (story.UserId == userId)
            {
                this.storyRepository.Delete(story);
                await this.storyRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAllStories<T>(int page , int itemsPerPage)
        {
            var stories = this.storyRepository.AllAsNoTracking().Where(x => x.IsApproved)
                            .OrderByDescending(x => x.Id)
                            .Skip((page - 1) * itemsPerPage)
                            .Take(itemsPerPage)
                            .To<T>().ToList();

            return stories;
        }

        private static string CreatePathInRoot(string categoryName, string nickName)
        {
            return $"/UserImages/{categoryName}/{nickName}/";
        }
    }
}
