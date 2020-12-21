namespace AnimalShelter.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AnimalShelter.Common;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels;
    using AnimalShelter.Web.ViewModels.PostModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AdoptController : BaseController
    {

        private const string CategoryFileFolder = "Adopt";

        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IGetCountService countService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdoptController(
            IPostService postService,
            IUserService userService,
            IGetCountService countService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.postService = postService;
            this.userService = userService;
            this.countService = countService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAdoptPetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var webRoot = this.webHostEnvironment.WebRootPath;

            int postId = await this.postService.CreatePostAsync<CreateAdoptPetInputModel>(input, user, webRoot, CategoryFileFolder, input.Images);

            this.TempData["Message"] = $"{input.Name} е успешно добавен/а за осиновяване и изчаква одобрение от администратор";

            return this.Redirect($"/Pet/PetProfile?id={postId}");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var viewModel = this.postService.GetPostById<EditAdoptPetInputModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAdoptPetInputModel input)
        {
            int countPhotos = this.countService.GetCurrentPostPhotosCount(input.Id);
            int countPhotosToUpload = input.Images?.ToList().Count() ?? 0;

            if ((countPhotos + countPhotosToUpload) > GlobalConstants.MaxPostPhotosUserCanUpload)
            {
                string photosLeft = GlobalConstants.MaxPostPhotosUserCanUpload - countPhotos > 0 ? $"Остават ви {GlobalConstants.MaxPostPhotosUserCanUpload - countPhotos }" : "Не можете да качите повече снимки";

                this.ModelState.AddModelError("Images", $"Можете да качите максимум 20 снимки за един пост.{photosLeft}");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (await this.userService.IsUserAuthorized(input.Id, user))
            {
                var webRoot = this.webHostEnvironment.WebRootPath;

                await this.postService.UpdatePetPostAsync<EditAdoptPetInputModel>(input, webRoot, CategoryFileFolder, input.Images, input.Id);

                this.TempData["Message"] = $"Постът за {input.Name} е успешно променен";

                return this.Redirect($"/Pet/PetProfile?id={input.Id}");
            }
            else
            {
                return this.StatusCode(401);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (await this.userService.IsUserAuthorized(id, user))
            {
                await this.postService.ChangeStatusAsync(id);

                return this.Redirect($"/Search/SearchResults");
            }
            else
            {
                return this.StatusCode(401);
            }
        }
    }
}
