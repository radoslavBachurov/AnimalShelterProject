namespace AnimalShelter.Web.Controllers
{
    using System.Diagnostics;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService getCountService;

        public HomeController(IGetCountsService getCountService)
        {
            this.getCountService = getCountService;
        }

        public IActionResult Index()
        {
            var viewModel = this.getCountService.GetIndexCounts();
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
