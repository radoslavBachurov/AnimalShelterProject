namespace AnimalShelter.Services.Data
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.User;
    using System.Threading.Tasks;

    public interface IUserService
    {
        UserViewModel GetUserProfile(ApplicationUser user);

        Task<bool> IsUserAuthorized(int postId, ApplicationUser user);
    }
}
