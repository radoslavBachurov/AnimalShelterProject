namespace AnimalShelter.Data.Models
{
    using AnimalShelter.Data.Common.Models;

    public class OrganisationLink : BaseDeletableModel<int>
    {
        public string LinkName { get; set; }

        public string LinkHref { get; set; }

        public int DonateOrganisationId { get; set; }

        public virtual DonateOrganisation DonateOrganisation { get; set; }
    }
}
