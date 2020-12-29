namespace AnimalShelter.Web.ViewModels.Administration.Donate
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;

    public class OrganisationLinkViewModel : IMapFrom<OrganisationLink>
    {
        public string LinkName { get; set; }

        public string LinkHref { get; set; }
    }
}
