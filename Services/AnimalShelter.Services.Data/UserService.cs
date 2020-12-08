namespace AnimalShelter.Services.Data
{
    using System;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.User;

    public class UserService : IUserService
    {
        public UserViewModel GetUserProfile(ApplicationUser user)
        {
            var userViewModel = AutoMapperConfig.MapperInstance.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
