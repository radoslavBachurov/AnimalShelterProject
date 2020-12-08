namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class AdoptListController : ControllerBase
    {
        private readonly IGetCountService getCountService;
        private readonly ISearchService searchService;

        public AdoptListController(IGetCountService getCountService, ISearchService searchService)
        {
            this.getCountService = getCountService;
            this.searchService = searchService;
        }

        public ActionResult<PetListViewModel> AllAnimals(string type, int page, string order, string orderType)
        {
            const int itemsPerPage = 4;
            var defaultValue = "Всички";
            var category = "За Осиновяване";

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.AnimalCount = this.getCountService.GetAllAnimalsByCriteriaCount(type, defaultValue, defaultValue, category);
            viewModel.Animals = this.searchService.GetAllAnimalsByCriteria<PetInListViewModel>(type, defaultValue, defaultValue, category, page, itemsPerPage, order, orderType);

            return viewModel;
        }
    }
}
