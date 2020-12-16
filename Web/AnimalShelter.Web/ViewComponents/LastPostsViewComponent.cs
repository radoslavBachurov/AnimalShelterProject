namespace AnimalShelter.Web.ViewComponents
{
    using System.Linq;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class LastPostsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;

        public LastPostsViewComponent(IDeletableEntityRepository<PetPost> petPostsRepository)
        {
            this.petPostsRepository = petPostsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var randomPosts = this.petPostsRepository.All()
              .Where(x => x.IsApproved && x.PetStatus == EnumHelper<PetStatus>.ParseEnum("ForAdoption")).To<LastPostViewComponentModel>()
              .OrderByDescending(x => x.Id)
              .Take(6).ToList();

            return this.View(randomPosts);
        }
    }
}
