namespace AnimalShelter.Services.Data
{
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.Home;

    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;
        private readonly IDeletableEntityRepository<SuccessStory> successStoriesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> users;

        public GetCountService(
        IDeletableEntityRepository<PetPost> petPostsRepository,
        IDeletableEntityRepository<Picture> pictureRepository,
        IDeletableEntityRepository<SuccessStory> successStoriesRepository,
        IDeletableEntityRepository<ApplicationUser> users)
        {
            this.petPostsRepository = petPostsRepository;
            this.pictureRepository = pictureRepository;
            this.successStoriesRepository = successStoriesRepository;
            this.users = users;
        }

        public IndexViewModel GetIndexCounts()
        {
            var data = new IndexViewModel()
            {
                DogsCount = this.petPostsRepository.AllAsNoTracking()
                            .Where(x => x.PetStatus == PetStatus.ForAdoption && x.Type == TypePet.Dog && x.IsApproved == true).Count(),

                CatCount = this.petPostsRepository.AllAsNoTracking()
                            .Where(x => x.PetStatus == PetStatus.ForAdoption && x.Type == TypePet.Cat && x.IsApproved == true).Count(),

                OtherAnimalsCount = this.petPostsRepository.AllAsNoTracking()
                            .Where(x => x.PetStatus == PetStatus.ForAdoption && x.Type == TypePet.Other && x.IsApproved == true).Count(),

                AdoptedAnimals = this.petPostsRepository.AllAsNoTracking().Where(x => x.PetStatus == PetStatus.Adopted).Count(),

                Volunteers = this.users.AllAsNoTracking().Count(),

                HappyStories = this.successStoriesRepository.AllAsNoTracking().Where(x => x.IsApproved == true)
                                .Select(x => new HappyEndingsIndexViewModel()
                                {
                                    Description = x.Description,
                                    Avatar = x.PostPictures.FirstOrDefault(x => x.IsCoverPicture),
                                    Likes = x.Likes,
                                    PersonName = x.PersonName,
                                    PetName = x.PetName,
                                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                                }).ToList(),
            };

            return data;
        }

        public int GetAllAnimalsByCriteriaCount(string typeAnimal, string sex, string location, string category)
        {
            var animalType = EnumHelper<TypePet>.GetValueFromName(typeAnimal);
            var animalSex = EnumHelper<Sex>.GetValueFromName(sex);
            var animalLocation = EnumHelper<City>.GetValueFromName(location);
            var animalCategory = EnumHelper<PetStatus>.GetValueFromName(category);

            var petsCount = this.petPostsRepository.AllAsNoTracking()
                           .Where(x => (animalCategory == 0 || x.PetStatus == animalCategory)
                           && (animalLocation == 0 || x.Location == animalLocation)
                           && (animalSex == 0 || x.Sex == animalSex)
                           && (animalType == 0 || x.Type == animalType)
                           && x.IsApproved == true).ToList().Count();

            return petsCount;
        }

        public int GetCurrentPostPhotosCount(int id)
        {
            var picCount = this.petPostsRepository.All()
                           .Select(x => new { PictureCount = x.PostPictures.Count(), Id = x.Id })
                           .Where(x => x.Id == id).FirstOrDefault();

            return picCount.PictureCount;
        }

        public int GetCurrentUserPhotosCount(string id)
        {
            var picCount = this.users.All()
                           .Select(x => new { PictureCount = x.UserPictures.Count(), x.Id })
                           .Where(x => x.Id == id).FirstOrDefault();

            return picCount.PictureCount;
        }
    }
}
