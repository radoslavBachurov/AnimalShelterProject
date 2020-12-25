namespace AnimalShelter.Web.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.StoriesModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HappyStoriesController : BaseController
    {
        private const string CategoryFileFolder = "HappyStories";
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHappyStoriesService storyService;

        public HappyStoriesController(
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            IHappyStoriesService storyService)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.storyService = storyService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public async Task<IActionResult> StoryProfile(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.storyService.GetStoryProfile(id, user);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateStoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var webRoot = this.webHostEnvironment.WebRootPath;

            int postId = await this.storyService.CreateStoryAsync<CreateStoryInputModel>(input, user, webRoot, CategoryFileFolder, input.Images);

            this.TempData["Message"] = $"Историята ви е успешно добавен/а и изчаква одобрение от администратор";

            return this.RedirectToAction(nameof(StoryProfile), new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.storyService.DeleteStoryAsync(id, user.Id);

            return this.RedirectToAction(nameof(All));
        }
    }
}
