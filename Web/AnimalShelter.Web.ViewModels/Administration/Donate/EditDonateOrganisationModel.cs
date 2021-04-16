namespace AnimalShelter.Web.ViewModels.Administration.Donate
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class EditDonateOrganisationModel : IMapTo<DonateOrganisation>, IMapFrom<DonateOrganisation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително поле")]
        [Display(Name = "Име на организацията...")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Описанието трябва да е от 2 до 100 символа")]
        public string Organisation { get; set; }

        [Required(ErrorMessage = "Описанието е задължително поле")]
        [Display(Name = "Описание...")]
        [DataType(DataType.MultilineText)]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Описанието трябва да е от 10 до 5000 символа")]
        public string Description { get; set; }

        [Display(Name = "Добави линкове на организацията")]
        public virtual ICollection<CreateOrganisationLinkInputModel> OrganisationLinks { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreateDonateOrganisationInputModel, DonateOrganisation>()
                .ForMember(x => x.OrganisationLinks, opt => opt.MapFrom(x => x.OrganisationLinks));
        }
    }
}
