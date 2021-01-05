namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Common;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private const string CategoryFileFolder = "Users";
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGetCountService getCountService;
        private readonly ISearchService searchService;
        private readonly IUserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserInfoController(
            UserManager<ApplicationUser> userManager,
            IGetCountService getCountService,
            ISearchService searchService,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.getCountService = getCountService;
            this.searchService = searchService;
            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public ActionResult<PetListViewModel> UserAnimals(string category, int page, string nick)
        {
            const int itemsPerPage = 4;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.AnimalCount = this.getCountService.GetAllUserAnimalsCountByCategory(category, nick);
            viewModel.Animals = this.searchService.GetAllUserAnimalsByCategory<PetInListViewModel>(category, page, itemsPerPage, nick);

            return viewModel;
        }

        [HttpPost("info")]
        public async Task<ActionResult> GetInfo([FromForm] UserInfoInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (this.userService.IsUsernameTakenForRegisteredUsers(inputModel.Nickname, user.Id))
            {
                this.ModelState.AddModelError("Nickname", "Потребителското име вече е заето");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.userService.UpdateUserInfoAsync(inputModel, user);

            return this.Ok(new { redirectToUrl = this.Url.Action("UserProfile", "User") });
        }

        [HttpPost("images")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetPictures([FromForm] UserImageInputModel uploadPhotos)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            int countPhotos = this.getCountService.GetCurrentUserPhotosCount(user.Id);
            int countPhotosToUpload = uploadPhotos.Images?.ToList().Count() ?? 0;

            if ((countPhotos + countPhotosToUpload) > GlobalConstants.MaxUserPhotosUserCanUpload)
            {
                string photosLeft = GlobalConstants.MaxUserPhotosUserCanUpload - countPhotos > 0 ? string.Format(ErrorMessages.LeftPhotosToUpload, GlobalConstants.MaxUserPhotosUserCanUpload - countPhotos) : ErrorMessages.PhotosLimitReached;

                this.ModelState.AddModelError("Images", string.Format(ErrorMessages.MaximumPhotosLeftToUpload, GlobalConstants.MaxUserPhotosUserCanUpload, photosLeft));
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var webRoot = this.webHostEnvironment.WebRootPath;
            await this.userService.UpdateUserImagesAsync(user, webRoot, CategoryFileFolder, uploadPhotos.Images);

            return this.Ok(new { redirectToUrl = this.Url.Action("UserProfile", "User") });
        }
    }
}
