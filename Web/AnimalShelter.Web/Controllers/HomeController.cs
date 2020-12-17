namespace AnimalShelter.Web.Controllers
{
    using System.Diagnostics;

    using AnimalShelter.Services.Data;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountService getCountService;
        private ErrorMessageGenerator messageGenerator;

        public HomeController(IGetCountService getCountService)
        {
            this.getCountService = getCountService;
            this.messageGenerator = new ErrorMessageGenerator();
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

        public IActionResult StatusCodeError(int errorCode)
        {
            var info = this.messageGenerator.GenerateErrorInfo(errorCode);
            return this.View(info);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
