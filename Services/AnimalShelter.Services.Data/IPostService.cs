namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IPostService
    {
        Task<int> CreatePostAsync<T>(T input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images);
    }
}
