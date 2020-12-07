namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        IEnumerable<T> GetAllAnimalsByCriteria<T>(string typeAnimal, string sex, string location, string category, int pageNumber, int itemsPerPage, string orderByProperty, string orderAscDesc);
    }
}
