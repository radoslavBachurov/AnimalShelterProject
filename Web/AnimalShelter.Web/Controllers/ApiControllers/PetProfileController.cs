using AnimalShelter.Data.Common.Repositories;
using AnimalShelter.Data.Models;
using AnimalShelter.Services.Data;
using AnimalShelter.Web.ViewModels.Pet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelter.Web.Controllers.ApiControllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PetProfileController : ControllerBase
    {
        private readonly IPetProfileService petService;
        private readonly UserManager<ApplicationUser> userManager;

        public PetProfileController(IPetProfileService petService, UserManager<ApplicationUser> userManager)
        {
            this.petService = petService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<LikeOutputModel>> AddLikeAsync(LikeInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var output = await this.petService.AddRemoveLikeToPostAsync(inputModel, user);
            return output;
        }
    }
}
