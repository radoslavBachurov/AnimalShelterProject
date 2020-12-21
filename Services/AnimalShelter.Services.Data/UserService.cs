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
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
                           IDeletableEntityRepository<PetPost> petPostsRepository,
                           IDeletableEntityRepository<ApplicationUser> userRepository,
                           UserManager<ApplicationUser> userManager)
        {
            this.petPostsRepository = petPostsRepository;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public UserViewModel GetUserProfile(string userId)
        {
            var userViewModel = this.userRepository.All().Where(x => x.Id == userId).To<UserViewModel>().FirstOrDefault();

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

        public bool IsUsernameTaken(string nickname)
        {
            var isUserTaken = this.userRepository.AllAsNoTracking().Where(x => x.Nickname == nickname).ToList();

            return isUserTaken.Any();
        }
    }
}
