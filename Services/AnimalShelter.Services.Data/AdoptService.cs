namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.Adopt;

    public class AdoptService : IAdoptService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private ImageBuilder imageBuilder;

        public AdoptService(IDeletableEntityRepository<PetPost> petPostsRepository)
        {
            this.petPostsRepository = petPostsRepository;
            this.imageBuilder = new ImageBuilder();
        }

        public async Task CreateAdoptionPostAsync(CreateAdoptPetInputModel input, ApplicationUser user, string webRoot)
        {
            var webRootPath = $"/UserImages/Adopt/{user.Nickname}/";
            var directory = webRoot + webRootPath;

            Directory.CreateDirectory(directory);

            var newAdoptPost = AutoMapperConfig.MapperInstance.Map<PetPost>(input);
            newAdoptPost.UserId = user.Id;
            newAdoptPost.PetStatus = PetStatus.ForAdoption;

            var pictures = await this.imageBuilder.CreatePicturesAsync(input.Images, webRootPath, user.Id, directory);
            pictures.ForEach(x => { newAdoptPost.PostPictures.Add(x); });

            await this.petPostsRepository.AddAsync(newAdoptPost);
            await this.petPostsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllAnimalsForAdoption<T>(int pageNumber, int itemsPerPage, string orderByProperty, string orderAscDesc)
        {
            var pets = this.petPostsRepository.AllAsNoTracking()
                .Where(x => x.IsApproved == true);

            var orderedByParameter = pets.OrderBy(orderByProperty, orderAscDesc);

            return orderedByParameter
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllAnimalsForAdoptionByType<T>(int pageNumber, int itemsPerPage, string typeAnimal, string orderByProperty, string orderAscDesc)
        {
            TypePet type = TypePet.Dog;

            switch (typeAnimal)
            {
                case "cats":
                    type = TypePet.Cat;
                    break;
                case "other":
                    type = TypePet.Other;
                    break;
                default:
                    break;
            }

            var pets = this.petPostsRepository.AllAsNoTracking()
                           .Where(x => x.Type == type && x.PetStatus == PetStatus.ForAdoption && x.IsApproved == true);

            var orderedByParameter = pets.OrderBy(orderByProperty, orderAscDesc);

            return orderedByParameter
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }
    }
}
