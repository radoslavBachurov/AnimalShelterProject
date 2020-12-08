namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        public IActionResult SearchResults()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult SearchResults(SearchResultsInputModel input)
        {
            return this.View(input);
        }
    }
}
