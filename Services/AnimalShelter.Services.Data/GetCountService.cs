using AnimalShelter.Data.Common.Repositories;
using AnimalShelter.Data.Models;
using AnimalShelter.Data.Models.Enums;
using AnimalShelter.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Services.Data
{
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

        public int GetAllAnimalsForAdoptionByTypeCount(string type)
        {
            TypePet typeAnimal = TypePet.Dog;

            switch (type)
            {
                case "cats":
                    typeAnimal = TypePet.Cat;
                    break;
                case "other":
                    typeAnimal = TypePet.Other;
                    break;
                default:
                    break;
            }

            var count = this.petPostsRepository.AllAsNoTracking()
                           .Where(x => x.PetStatus == PetStatus.ForAdoption && x.Type == typeAnimal && x.IsApproved == true).Count();

            return count;
        }

        public int GetAllAnimalsForAdoptionCount()
        {
            var count = this.petPostsRepository.AllAsNoTracking()
                           .Where(x => x.PetStatus == PetStatus.ForAdoption && x.IsApproved == true).Count();

            return count;
        }
    }
}
