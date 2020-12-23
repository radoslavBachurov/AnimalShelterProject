namespace AnimalShelter.Web.ViewModels.SearchResults
{
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AutoMapper;

    public class PetInListViewModel : IMapFrom<PetPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Sex { get; set; }

        public string Type { get; set; }

        public string PetStatus { get; set; }

        public string CreatedOn { get; set; }

        public string CoverPicturePath { get; set; }

        public string IsApproved { get; set; }

        public string Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetPost, PetInListViewModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.PostPictures.Where(x => x.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                            x.PostPictures.Where(x => x.IsCoverPicture).FirstOrDefault().Path :
                                                                            x.PostPictures.Where(x => x.IsCoverPicture).FirstOrDefault().RemoteImageUrl))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description.Substring(0, 60) + "..."))
                .ForMember(x => x.PetStatus, opt => opt.MapFrom(x => EnumHelper<PetStatus>.GetDisplayValue(x.PetStatus)))
                .ForMember(x => x.Sex, opt => opt.MapFrom(x => EnumHelper<Sex>.GetDisplayValue(x.Sex)))
                .ForMember(x => x.IsApproved, opt => opt.MapFrom(x => x.IsApproved ? "Одобрен" : "Чака одобрение"));
        }
    }
}
