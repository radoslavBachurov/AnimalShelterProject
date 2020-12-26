namespace AnimalShelter.Web.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PetController : BaseController
    {
        private readonly IPetProfileService petService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public PetController(
            IPetProfileService petService,
            IUserService userService,
            IPostService postService,
            UserManager<ApplicationUser> userManager)
        {
            this.petService = petService;
            this.userService = userService;
            this.postService = postService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> PetProfile(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.petService.GetPetProfile(id, user);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (await this.userService.IsUserPostAuthorized(id, user))
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
