namespace AnimalShelter.Services.Data.AdministrationServices
{
    using System.Threading.Tasks;

    using AnimalShelter.Web.ViewModels.Administration.Donate;

    public interface IDonateService
    {
        Task AddDonateOrganisationAsync(CreateDonateOrganisationInputModel input);
    }
}
