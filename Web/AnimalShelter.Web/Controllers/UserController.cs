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
            await this.userService.DeletePictureAsync(id,user.Id);

            return this.RedirectToAction(nameof(ChangeProfilePic));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProfile(UserViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            int countPhotos = this.getCountService.GetCurrentUserPhotosCount(user.Id);
            int countPhotosToUpload = input.Images?.ToList().Count() ?? 0;

            if ((countPhotos + countPhotosToUpload) > GlobalConstants.MaxUserPhotosUserCanUpload)
            {
                string photosLeft = GlobalConstants.MaxPostPhotosUserCanUpload - countPhotos > 0 ? $"Остават ви {GlobalConstants.MaxUserPhotosUserCanUpload - countPhotos }" : "Не можете да качите повече снимки";

                this.ModelState.AddModelError("Images", $"Можете да качите максимум 100 снимки.{photosLeft}");
            }

            if (this.userService.IsUsernameTakenForRegisteredUsers(input.InputNickname, user.Id))
            {
                this.ModelState.AddModelError("InputNickname", "Потребителското име вече е заето");
            }

            if (!this.ModelState.IsValid)
            {
                var viewModel = this.userService.GetUserProfile(user.Id);
                return this.View(viewModel);
            }

            var webRoot = this.webHostEnvironment.WebRootPath;

            await this.userService.UpdateUserInfo(input, user, webRoot, CategoryFileFolder, input.Images);
            return this.RedirectToAction(nameof(UserProfile));
        }
    }
}
