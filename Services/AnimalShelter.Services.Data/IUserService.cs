namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Http;

    public interface IUserService
    {
        Task ClearNotificationsAsync(string userId);

        UserViewModel GetUserProfile(string userId);

        Task<bool> IsUserPostAuthorized(int postId, ApplicationUser user);

        bool IsUserProfileOperationAuthorized(string pictureOwner, string currentUserOwner);

        bool IsUsernameTaken(string nickname);

        bool IsUsernameTakenForRegisteredUsers(string nickname, string userId);

        Task SetProfilePictureAsync(string pictureId, string userId);

        Task DeletePictureAsync(string pictureId, string userId);

        T GetUserByNickName<T>(string nickName);

        IEnumerable<T> GetNotifications<T>(string userId, int pageNumber, int itemsPerPage);

        IEnumerable<T> GetAllUserProfilePics<T>(string id);

        Task UpdateUserInfo(UserViewModel input, ApplicationUser user, string webRoot, string categoryName, IEnumerable<IFormFile> images);
    }
}
