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

        public ActionResult<PetListViewModel> AllAnimals(string type, int page = 1, string order = "Id", string orderType = "desc")
        {
            const int itemsPerPage = 4;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            if (type == "all")
            {
                viewModel.AnimalCount = this.getCountService.GetAllAnimalCount();
                viewModel.Animals = this.adoptService.GetAll<PetInListViewModel>(page, itemsPerPage, order, orderType);
            }
            else
            {
                //Create allanimalsCountbyType
                viewModel.AnimalCount = this.getCountService.GetAdoptDogCount();
                viewModel.Animals = this.adoptService.GetAllAnimalsByType<PetInListViewModel>(page, itemsPerPage, type, order, orderType);
            }

            return viewModel;
        }
    }
}
