namespace AnimalShelter.Web.ViewModels.Administration.Donate
{
    using System.Collections.Generic;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;

    public class DonateOrganisationUserViewModel : IMapFrom<DonateOrganisation>
    {
        public string Organisation { get; set; }

        public string Description { get; set; }

        public virtual ICollection<OrganisationLinkViewModel> OrganisationLinks { get; set; }
    }
}
