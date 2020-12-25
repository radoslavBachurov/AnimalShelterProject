namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class PetPostListController : ControllerBase
    {
        private readonly IGetCountService getCountService;
        private readonly ISearchService searchService;

        public PetPostListController(IGetCountService getCountService, ISearchService searchService)
        {
            this.getCountService = getCountService;
            this.searchService = searchService;
        }

        public ActionResult<PetListViewModel> AllAnimals(string info, int page, string order, string orderType)
        {
            const int itemsPerPage = 4;
            var defaultValue = "Всички";

            var type = "Всички";
            string category;

            if (info == "Котка" || info == "Куче" || info == "Други")
            {
                type = info;
                category = "За Осиновяване";
            }
            else
            {
                category = info;
            }

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
