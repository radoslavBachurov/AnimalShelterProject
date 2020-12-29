namespace AnimalShelter.Services.Data.AdministrationServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.Administration.Donate;

    public class DonateService : IDonateService
    {
        private readonly IDeletableEntityRepository<DonateOrganisation> donatePostsRepository;

        public DonateService(IDeletableEntityRepository<DonateOrganisation> donatePostsRepository)
        {
            this.donatePostsRepository = donatePostsRepository;
        }

        public async Task AddDonateOrganisationAsync(CreateDonateOrganisationInputModel input, string userId)
        {
            var newOrganisation = AutoMapperConfig.MapperInstance.Map<DonateOrganisation>(input);
            newOrganisation.UserId = userId;

            await this.donatePostsRepository.AddAsync(newOrganisation);
            await this.donatePostsRepository.SaveChangesAsync();
        }

        public List<T> GetAllOrganisations<T>()
        {
            var viewModel = this.donatePostsRepository.AllAsNoTracking().To<T>().ToList();

            return viewModel;
        }
    }
}
