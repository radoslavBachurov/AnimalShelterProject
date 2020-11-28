namespace AnimalShelter.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
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
        private readonly IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository;
        private ImageBuilder imageBuilder;

        public AdoptService(IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository)
        {
            this.adoptionPostsRepository = adoptionPostsRepository;
            this.imageBuilder = new ImageBuilder();
        }

        public async Task CreateAdoptionPost(CreateAdoptPetInputModel input, ApplicationUser user, string webRoot)
        {
            var webRootPath = $"/UserImages/Adopt/{user.Nickname}/";
            var directory = webRoot + webRootPath;

            Directory.CreateDirectory(directory);

            var newAdoptPost = new PetAdoptionPost()
            {
                Description = input.Description,
                Location = input.Location,
                Sex = input.Sex,
                Type = input.Type,
                UserId = user.Id,
                Name = input.Name,
            };

            var pictures = await this.imageBuilder.CreatePictures(input.Images, webRootPath, user.Id, directory);
            pictures.ForEach(x => { newAdoptPost.PostPictures.Add(x); });

            await this.adoptionPostsRepository.AddAsync(newAdoptPost);
            await this.adoptionPostsRepository.SaveChangesAsync();
        }

        IEnumerable<T> IAdoptService.GetAll<T>(int pageNumber, int itemsPerPage)
        {
            var pets = this.adoptionPostsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return pets;
        }

        IEnumerable<T> IAdoptService.GetAllDogs<T>(int pageNumber, int itemsPerPage)
        {
            var pets = this.adoptionPostsRepository.AllAsNoTracking()
                .Where(x => x.Type == TypePet.Dog)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return pets;
        }

        IEnumerable<T> IAdoptService.GetAllCats<T>(int pageNumber, int itemsPerPage)
        {
            var pets = this.adoptionPostsRepository.AllAsNoTracking()
                .Where(x => x.Type == TypePet.Cat)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return pets;
        }

        IEnumerable<T> IAdoptService.GetAllOther<T>(int pageNumber, int itemsPerPage)
        {
            var pets = this.adoptionPostsRepository.AllAsNoTracking()
                .Where(x => x.Type == TypePet.Other)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return pets;
        }
    }
}
