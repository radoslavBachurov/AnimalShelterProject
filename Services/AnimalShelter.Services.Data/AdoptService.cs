namespace AnimalShelter.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
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

        public async Task CreateAdoptionPost(CreatePetInputModel input, ApplicationUser user, string webRoot)
        {
            var directory = webRoot + $"/UserImages/Adopt/{user.Nickname}/";

            Directory.CreateDirectory(directory);

            var newAdoptPost = new PetAdoptionPost()
            {
                Description = input.Description,
                Location = input.Location,
                Sex = input.Sex,
                Type = input.Type,
                UserId = user.Id,
            };

            foreach (var image in input.Images)
            {
                var picture = new Picture() { Path = directory, UserId = user.Id };

                string extension = await this.imageBuilder.SaveImage(image, directory, picture.Id);

                picture.Extension = extension;
                newAdoptPost.PostPictures.Add(picture);
            }

            await this.adoptionPostsRepository.AddAsync(newAdoptPost);
            await this.adoptionPostsRepository.SaveChangesAsync();
        }
    }
}
