namespace AnimalShelter.Services.Data
{
    using AnimalShelter.Web.ViewModels.Home;

    public interface IGetCountService
    {
        IndexViewModel GetIndexCounts();

        int GetAllAnimalsByCriteriaCount(string typeAnimal, string sex, string location, string category);

        int GetCurrentPostPhotosCount(int id);

        int GetCurrentUserPhotosCount(string id);

        int GetAllUserAnimalsCountByCategory(string category, string nickName);

        int GetAllStoriesCount();
    }
}
