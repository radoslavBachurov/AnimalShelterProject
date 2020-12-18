namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class SearchListController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly IGetCountService getCountService;

        public SearchListController(ISearchService searchService, IGetCountService getCountService)
        {
            this.searchService = searchService;
            this.getCountService = getCountService;
        }

        public ActionResult<PetListViewModel> GetResults(string type, string sex, string location, string category, int page, string order, string ordertype)
        {
            const int itemsPerPage = 5;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.AnimalCount = this.getCountService.GetAllAnimalsByCriteriaCount(type, sex, location, category);
            viewModel.Animals = this.searchService.GetAllAnimalsByCriteria<PetInListViewModel>(type, sex, location, category, page, itemsPerPage, order, ordertype);
            viewModel.UrlInfo = this.searchService.GetUrlInfo(type, sex, location, category);
            return viewModel;
        }
    }
}
