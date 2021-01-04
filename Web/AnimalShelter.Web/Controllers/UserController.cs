namespace AnimalShelter.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Common;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private const string CategoryFileFolder = "Users";
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IGetCountService getCountService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IGetCountService getCountService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.getCountService = getCountService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.userService.GetUserProfile(user.Id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult UserProfileGuest(string nickName)
        {
            var viewModel = this.userService.GetUserByNickName<UserGuestViewModel>(nickName);

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ChangeProfilePic()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var pictures = this.userService.GetAllUserProfilePics<UserProfilePicViewModel>(user.Id);

            return this.View(pictures);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePic(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userService.SetProfilePictureAsync(id, user.Id);

            return this.RedirectToAction(nameof(UserProfile));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePic(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userService.DeletePictureAsync(id, user.Id);

            return this.RedirectToAction(nameof(ChangeProfilePic));
        }

        [Authorize]
        public async Task<IActionResult> Notifications(int page = 1)
        {
            const int itemsPerPage = 5;

            var viewModel = new UserListNotificationViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.Count = this.getCountService.GetNotificationsCount(user.Id);
            viewModel.Notifications = this.userService.GetNotifications<UserNotificationViewModel>(user.Id, page, itemsPerPage);

            return this.View(viewModel);
        }
    }
}
