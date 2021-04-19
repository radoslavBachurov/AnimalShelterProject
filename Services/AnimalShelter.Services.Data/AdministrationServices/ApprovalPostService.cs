namespace AnimalShelter.Services.Data.AdministrationServices
{
    using System.Collections.Generic;
    using System.Linq;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;

    public class ApprovalPostService : IApprovalPostService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private readonly IDeletableEntityRepository<SuccessStory> successStoriesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> users;

        public ApprovalPostService(
        IDeletableEntityRepository<PetPost> petPostsRepository,
        IDeletableEntityRepository<SuccessStory> successStoriesRepository,
        IDeletableEntityRepository<ApplicationUser> users)
        {
            this.petPostsRepository = petPostsRepository;
            this.successStoriesRepository = successStoriesRepository;
            this.users = users;
        }

        public List<T> GetAllAdoptPostsForApproval<T>(int page, int itemsPerPage)
        {
            var adoptPostsForApproval = this.petPostsRepository.AllAsNoTracking()
                .Where(x => (x.PetStatus == PetStatus.Adopted || x.PetStatus == PetStatus.ForAdoption) && !x.IsApproved)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return adoptPostsForApproval;
        }

        public List<T> GetAllLostFoundPostsForApproval<T>(int page, int itemsPerPage)
        {
            throw new System.NotImplementedException();
        }

        public List<T> GetAllStoriesPostsForApproval<T>(int page, int itemsPerPage)
        {
            throw new System.NotImplementedException();
        }
    }
}
