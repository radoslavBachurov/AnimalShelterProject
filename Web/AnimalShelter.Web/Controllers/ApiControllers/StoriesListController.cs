namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels.StoriesModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class StoriesListController : ControllerBase
    {
        private readonly IGetCountService getCountService;
        private readonly IHappyStoriesService storyService;

        public StoriesListController(IGetCountService getCountService, IHappyStoriesService storyService)
        {
            this.getCountService = getCountService;
            this.storyService = storyService;
        }

        public ActionResult<StoryListViewModel> HappyStoriesList(int page)
        {
            const int itemsPerPage = 12;

            var viewModel = new StoryListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
            };

            viewModel.StoryCount = this.getCountService.GetAllStoriesCount();
            viewModel.Stories = this.storyService.GetAllStories<StoryInListViewModel>(page, itemsPerPage);

            return viewModel;
        }
    }
}
