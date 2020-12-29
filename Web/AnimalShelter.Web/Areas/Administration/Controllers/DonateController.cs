namespace AnimalShelter.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data.AdministrationServices;
    using AnimalShelter.Web.ViewModels.Administration.Donate;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DonateController : AdministrationController
    {
        private readonly IDonateService donateService;
        private readonly UserManager<ApplicationUser> userManager;

        public DonateController(IDonateService donateService, UserManager<ApplicationUser> userManager)
        {
            this.donateService = donateService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult All()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDonateOrganisationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.donateService.AddDonateOrganisationAsync(input, user.Id);

            return this.RedirectToAction(nameof(All));
        }
    }
}
