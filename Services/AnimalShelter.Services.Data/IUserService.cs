namespace AnimalShelter.Services.Data
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        UserViewModel GetUserProfile(string userId);

        Task<bool> IsUserAuthorized(int postId, ApplicationUser user);

        bool IsUsernameTaken(string nickname);

        bool IsUsernameTakenForRegisteredUsers(string nickname, string userId);

        Task SetProfilePictureAsync(string pictureId, string userId);

        IEnumerable<T> GetAllUserProfilePics<T>(string id);

        Task UpdateUserInfo(UserViewModel input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images);
    }
}
