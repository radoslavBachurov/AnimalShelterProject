namespace AnimalShelter.Web.ViewModels.Pet
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class PetProfileViewModel : IMapFrom<PetAdoptionPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public int Likes { get; set; }

        public string Location { get; set; }

        public string Sex { get; set; }

        public string CreatedOn { get; set; }

        public string CoverPicturePath { get; set; }

        public IEnumerable<PetProfilePicViewModel> Pictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetAdoptionPost, PetProfileViewModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Pictures, opt => opt.MapFrom(x => x.PostPictures))
                .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                            x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                           x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
