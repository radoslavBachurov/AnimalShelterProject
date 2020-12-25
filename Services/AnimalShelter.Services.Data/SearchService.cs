namespace AnimalShelter.Services.Data
{

    using System.Collections.Generic;
    using System.Linq;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.Infrastructure.Enums;
    using AnimalShelter.Web.ViewModels.SearchResults;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;

        public SearchService(IDeletableEntityRepository<PetPost> petPostsRepository)
        {
            this.petPostsRepository = petPostsRepository;
        }

        // For Adopt,LostFound,Search pages
        public IEnumerable<T> GetAllAnimalsByCriteria<T>(string typeAnimal, string sex, string location, string category, int pageNumber, int itemsPerPage, string orderByProperty, string orderAscDesc)
        {
            var animalType = EnumHelper<TypePet>.GetValueFromName(typeAnimal);
            var animalSex = EnumHelper<Sex>.GetValueFromName(sex);
            var animalLocation = EnumHelper<City>.GetValueFromName(location);
            var animalCategory = EnumHelper<PetStatus>.GetValueFromName(category);

            var pets = this.petPostsRepository.AllAsNoTracking()
                           .Where(x => (animalCategory == 0 || x.PetStatus == animalCategory)
                           && (animalLocation == 0 || x.Location == animalLocation)
                           && (animalSex == 0 || x.Sex == animalSex)
                           && (animalType == 0 || x.Type == animalType)
                           && x.IsApproved == true);

            var orderedByParameter = pets.OrderBy(orderByProperty, orderAscDesc);

            return orderedByParameter
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public SearchResultsInputModel GetUrlInfo(string typeAnimal, string sex, string location, string category)
        {
            var urlInfo = new SearchResultsInputModel()
            {
                Type = EnumHelper<SearchTypePet>.GetValueFromName(typeAnimal),
                Sex = EnumHelper<SearchSex>.GetValueFromName(sex),
                Location = EnumHelper<SearchCity>.GetValueFromName(location),
                PetStatus = EnumHelper<SearchPetStatus>.GetValueFromName(category),
            };

            return urlInfo;
        }

        // For UserPage
        public IEnumerable<T> GetAllUserAnimalsByCategory<T>(string category, int page, int itemsPerPage, string nickName)
        {
            var petPosts = new List<T>();

            if (category == "MyPosts")
            {
                petPosts = this.petPostsRepository.AllAsNoTracking()
                            .Where(x => x.User.Nickname == nickName)
                            .OrderByDescending(x => x.Id)
                            .Skip((page - 1) * itemsPerPage)
                            .Take(itemsPerPage)
                            .To<T>()
                            .ToList();
            }
            else
            {
                petPosts = this.petPostsRepository.AllAsNoTracking()
                            .Where(x => x.UserLikes.Any(x => x.ApplicationUser.Nickname == nickName))
                            .OrderByDescending(x => x.Id)
                            .Skip((page - 1) * itemsPerPage)
                            .Take(itemsPerPage)
                            .To<T>()
                            .ToList();
            }

            return petPosts;
        }
    }
}
