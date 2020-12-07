namespace AnimalShelter.Web.Controllers
{
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Web.ViewModels.SearchResults;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        public IActionResult SearchResults(SearchResultsInputModel input)
        {
            if (input == null)
            {
                return this.View();
            }

            return this.View(input);
        }
    }
}
