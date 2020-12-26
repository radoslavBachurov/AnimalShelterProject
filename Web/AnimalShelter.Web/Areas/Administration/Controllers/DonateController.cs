namespace AnimalShelter.Web.Areas.Administration.Controllers
{
    using AnimalShelter.Services.Data.AdministrationServices;
    using AnimalShelter.Web.ViewModels.Administration.Donate;
    using Microsoft.AspNetCore.Mvc;

    public class DonateController : AdministrationController
    {
        private readonly IDonateService donateService;

        public DonateController(IDonateService donateService)
        {
            this.donateService = donateService;
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
        public IActionResult Create(CreateDonateOrganisationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.donateService.AddDonateOrganisationAsync(input);

            return this.RedirectToAction(nameof(All));
        }
    }
}
