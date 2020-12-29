namespace AnimalShelter.Services.Data.AdministrationServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AnimalShelter.Web.ViewModels.Administration.Donate;

    public interface IDonateService
    {
        Task AddDonateOrganisationAsync(CreateDonateOrganisationInputModel input, string userId);

        List<T> GetAllOrganisations<T>();
    }
}
