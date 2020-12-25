namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGetCountService getCountService;
        private readonly ISearchService searchService;

        public UserInfoController(UserManager<ApplicationUser> userManager, IGetCountService getCountService, ISearchService searchService)
        {
            this.userManager = userManager;
            this.getCountService = getCountService;
            this.searchService = searchService;
        }

        public ActionResult<PetListViewModel> UserAnimals(string category, int page, string nick)
        {
            const int itemsPerPage = 4;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.AnimalCount = this.getCountService.GetAllUserAnimalsCountByCategory(category, nick);
            viewModel.Animals = this.searchService.GetAllUserAnimalsByCategory<PetInListViewModel>(category, page, itemsPerPage, nick);

            return viewModel;
        }
    }
}
