namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;

    using AnimalShelter.Web.ViewModels.SearchResults;

    public interface ISearchService
    {
        IEnumerable<T> GetAllAnimalsByCriteria<T>(string typeAnimal, string sex, string location, string category, int pageNumber, int itemsPerPage, string orderByProperty, string orderAscDesc);

        SearchResultsInputModel GetUrlInfo(string typeAnimal, string sex, string location, string category);

        IEnumerable<T> GetAllUserAnimalsByCategory<T>(string category, int page,int itemsPerPage, string userId);
    }
}
