namespace AnimalShelter.Web.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.Adopt;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AdoptController : BaseController
    {
        private readonly IAdoptService adoptService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IGetCountService getCountService;

        public AdoptController(
            IAdoptService adoptService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            IGetCountService getCountService)
        {
            this.adoptService = adoptService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.getCountService = getCountService;
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

            await this.adoptService.CreateAdoptionPostAsync(input, user, webRoot);

            return this.RedirectToAction(nameof(All));
        }
    }
}
