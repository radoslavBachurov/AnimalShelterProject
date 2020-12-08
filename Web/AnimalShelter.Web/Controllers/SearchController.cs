namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        [HttpPost]
        public IActionResult SearchResults(SearchResultsInputModel input)
        {
            if (input == null)
            {
                return this.View();
            }

            return this.View(input);
        }

        [HttpGet]
        public IActionResult SearchResults()
        {
           return this.View();
        }
    }
}
