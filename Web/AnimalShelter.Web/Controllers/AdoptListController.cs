namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.Adopt;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class AdoptListController : ControllerBase
    {
        private readonly IAdoptService adoptService;
        private readonly IGetCountService getCountService;

        public AdoptListController(IAdoptService adoptService, IGetCountService getCountService)
        {
            this.adoptService = adoptService;
            this.getCountService = getCountService;
        }

        public ActionResult<PetListViewModel> AllAnimals(string type, int page = 1)
        {
            const int itemsPerPage = 1;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            switch (type)
            {
                case "cats":
                    viewModel.AnimalCount = this.getCountService.GetAdoptCatCount();
                    viewModel.Animals = this.adoptService.GetAllCats<PetInListViewModel>(page, itemsPerPage);
                    break;
                case "dogs":
                    viewModel.AnimalCount = this.getCountService.GetAdoptDogCount();
                    viewModel.Animals = this.adoptService.GetAllDogs<PetInListViewModel>(page, itemsPerPage);
                    break;
                case "other":
                    viewModel.AnimalCount = this.getCountService.GetAdoptOtherCount();
                    viewModel.Animals = this.adoptService.GetAllOther<PetInListViewModel>(page, itemsPerPage);
                    break;
                default:
                    break;
            }

            return viewModel;
        }
    }
}
