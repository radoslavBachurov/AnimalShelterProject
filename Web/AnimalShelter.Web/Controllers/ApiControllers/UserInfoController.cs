namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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

        public async Task<ActionResult<PetListViewModel>> UserAnimals(string category, int page)
        {
            const int itemsPerPage = 4;

            var viewModel = new PetListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            var user = await this.userManager.GetUserAsync(this.User);

            viewModel.AnimalCount = this.getCountService.GetAllUserAnimalsCountByCategory(category, user.Id);
            viewModel.Animals = this.searchService.GetAllUserAnimalsByCategory<PetInListViewModel>(category, page, itemsPerPage, user.Id);

            return viewModel;
        }
    }
}
