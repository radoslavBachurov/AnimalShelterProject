namespace AnimalShelter.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AnimalShelter.Common;
    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;


    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
                           IDeletableEntityRepository<PetPost> petPostsRepository,
                           UserManager<ApplicationUser> userManager)
        {
            this.petPostsRepository = petPostsRepository;
            this.userManager = userManager;
        }

        public UserViewModel GetUserProfile(ApplicationUser user)
        {
            var userViewModel = AutoMapperConfig.MapperInstance.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<bool> IsUserAuthorized(int postId, ApplicationUser user)
        {
            var postUserId = this.petPostsRepository.All().Where(x => x.Id == postId).Select(x => x.UserId).FirstOrDefault();

            if (postUserId == user.Id)
            {
                return true;
            }

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName))
            {
                return true;
            }

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.ModeratorRoleName))
            {
                return true;
            }

            return false;
        }
    }
}
