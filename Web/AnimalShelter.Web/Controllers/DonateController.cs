namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Services.Data.AdministrationServices;
    using AnimalShelter.Web.ViewModels.Administration.Donate;
    using Microsoft.AspNetCore.Mvc;

    public class DonateController : BaseController
    {
        private readonly IDonateService donateService;

        public DonateController(IDonateService donateService)
        {
            this.donateService = donateService;
        }

        public IActionResult All()
        {
            var view = this.donateService.GetAllOrganisations<DonateOrganisationUserViewModel>();
            return this.View(view);
        }
    }
}
