namespace AnimalShelter.Services.Data
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.User;

    public interface IUserService
    {
        UserViewModel GetUserProfile(ApplicationUser user);
    }
}
