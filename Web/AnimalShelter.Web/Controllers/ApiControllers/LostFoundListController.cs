namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class LostFoundListController : ControllerBase
    {
        private readonly IGetCountService getCountService;
        private readonly ISearchService searchService;

        public LostFoundListController(IGetCountService getCountService, ISearchService searchService)
        {
            this.getCountService = getCountService;
            this.searchService = searchService;
        }

        public ActionResult<PetListViewModel> AllAnimals(string category, int page, string order, string orderType)
        {
            const int itemsPerPage = 4;
            var defaultValue = "Всички";

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.AnimalCount = this.getCountService.GetAllAnimalsByCriteriaCount(defaultValue, defaultValue, defaultValue, category);
            viewModel.Animals = this.searchService.GetAllAnimalsByCriteria<PetInListViewModel>(defaultValue, defaultValue, defaultValue, category, page, itemsPerPage, order, orderType);

            return viewModel;
        }
    }
}
