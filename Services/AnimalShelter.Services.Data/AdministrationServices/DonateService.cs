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
        private readonly IDeletableEntityRepository<OrganisationLink> organisationLinkRepository;

        public DonateService(
            IDeletableEntityRepository<DonateOrganisation> donatePostsRepository,
            IDeletableEntityRepository<OrganisationLink> organisationLinkRepository)
        {
            this.donatePostsRepository = donatePostsRepository;
            this.organisationLinkRepository = organisationLinkRepository;
        }

        public async Task AddDonateOrganisationAsync(CreateDonateOrganisationInputModel input, string userId)
        {
            var newOrganisation = AutoMapperConfig.MapperInstance.Map<DonateOrganisation>(input);
            newOrganisation.UserId = userId;

            newOrganisation.OrganisationLinks = newOrganisation.OrganisationLinks
                           .Where(x => !string.IsNullOrWhiteSpace(x.LinkHref) && !string.IsNullOrWhiteSpace(x.LinkName)).ToList();

            await this.donatePostsRepository.AddAsync(newOrganisation);
            await this.donatePostsRepository.SaveChangesAsync();
        }

        public List<T> GetAllOrganisations<T>()
        {
            var viewModel = this.donatePostsRepository.AllAsNoTracking().To<T>().ToList();

            return viewModel;
        }

        public async Task Delete(int id)
        {
            var organisation = this.donatePostsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.donatePostsRepository.Delete(organisation);
            await this.donatePostsRepository.SaveChangesAsync();
        }

        public T GetOrganisationById<T>(int id)
        {
            var organisation = this.donatePostsRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return organisation;
        }

        public async Task EditDonateOrganisationAsync(EditDonateOrganisationModel input, string userId)
        {
            var organisation = this.donatePostsRepository.All().FirstOrDefault(x => x.Id == input.Id);
            var links = this.organisationLinkRepository.All().Where(x => x.DonateOrganisationId == organisation.Id).ToList();

            organisation.Organisation = input.Organisation;
            organisation.Description = input.Description;

            foreach (var link in links)
            {
                this.organisationLinkRepository.Delete(link);
            }

            if (input.OrganisationLinks != null)
            {
                foreach (var link in input.OrganisationLinks.Where(x => !string.IsNullOrWhiteSpace(x.LinkHref) && !string.IsNullOrWhiteSpace(x.LinkName)).ToList())
                {
                    var mappedLink = AutoMapperConfig.MapperInstance.Map<OrganisationLink>(link);
                    organisation.OrganisationLinks.Add(mappedLink);
                }
            }

            await this.donatePostsRepository.SaveChangesAsync();
            await this.organisationLinkRepository.SaveChangesAsync();
        }
    }
}
