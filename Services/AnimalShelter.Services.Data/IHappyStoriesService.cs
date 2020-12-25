namespace AnimalShelter.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.Pet;
    using AnimalShelter.Web.ViewModels.StoriesModels;
    using Microsoft.AspNetCore.Http;

    public interface IHappyStoriesService
    {
        Task<int> CreateStoryAsync<T>(T input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images);

        Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input, ApplicationUser user);

        StoryProfileViewModel GetStoryProfile(int id, ApplicationUser user);

        Task DeleteStoryAsync(int storyId, string userId);

        IEnumerable<T> GetAllStories<T>(int page, int itemsPerPage);
    }
}
