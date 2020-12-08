namespace AnimalShelter.Services.Data
{
    using AnimalShelter.Web.ViewModels.Home;

    public interface IGetCountService
    {
        IndexViewModel GetIndexCounts();

        int GetAllAnimalsByCriteriaCount(string typeAnimal, string sex, string location, string category);
    }
}
