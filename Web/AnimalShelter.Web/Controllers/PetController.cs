namespace AnimalShelter.Web.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PetController : BaseController
    {
        private readonly IPetProfileService petService;
        private readonly UserManager<ApplicationUser> userManager;

        public PetController(IPetProfileService petService, UserManager<ApplicationUser> userManager)
        {
            this.petService = petService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> PetProfile(int id)
        {
            var postId = id;
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.petService.GetPetProfile(postId, user);

            return this.View(viewModel);
        }
    }
}
