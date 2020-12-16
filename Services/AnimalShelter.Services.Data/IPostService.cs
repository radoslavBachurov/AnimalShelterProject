namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.PostModels;
    using Microsoft.AspNetCore.Http;

    public interface IPostService
    {
        Task<int> CreatePostAsync<T>(T input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images);

        T GetPostById<T>(int id);

        Task UpdatePetPostAsync<T>(T input, string webRoot, string categoryName, IEnumerable<IFormFile> images, int id);

        Task ChangeStatusAsync(int id);
    }
}
