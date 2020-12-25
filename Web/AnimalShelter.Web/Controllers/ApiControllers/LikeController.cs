namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.Pet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IPetProfileService petService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHappyStoriesService storyService;

        public LikeController(IPetProfileService petService, UserManager<ApplicationUser> userManager, IHappyStoriesService storyService)
        {
            this.petService = petService;
            this.userManager = userManager;
            this.storyService = storyService;
        }

        [HttpPost]
        public async Task<ActionResult<LikeOutputModel>> AddLikeAsync(LikeInputModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (inputModel.ToLike == "Post")
            {
                var output = await this.petService.AddRemoveLikeToPostAsync(inputModel, user);
                return output;
            }
            else
            {
                var output = await this.storyService.AddRemoveLikeToPostAsync(inputModel, user);
                return output;
            }
        }
    }
}
