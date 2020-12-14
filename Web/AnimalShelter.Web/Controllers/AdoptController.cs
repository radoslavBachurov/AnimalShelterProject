namespace AnimalShelter.Web.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.PostModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AdoptController : BaseController
    {

        private const string CategoryFileFolder = "Adopt";

        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdoptController(
            IPostService postService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.postService = postService;
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
    }
}
