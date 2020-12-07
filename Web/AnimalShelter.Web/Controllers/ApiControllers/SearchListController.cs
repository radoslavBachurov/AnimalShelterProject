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

        public SearchListController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public ActionResult<PetListViewModel> GetResults(string type, string sex, string location, string category, int page, string order, string ordertype)
        {
            const int itemsPerPage = 12;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.Animals = this.searchService.GetAllAnimalsByCriteria<PetInListViewModel>(type, sex, location, category, page, itemsPerPage, order, ordertype);

            return viewModel;
        }
    }
}
