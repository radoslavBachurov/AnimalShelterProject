namespace AnimalShelter.Web.ViewModels.Pet
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AutoMapper;

    public class PetProfileViewModel : IMapFrom<PetPost>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int Likes { get; set; }

        public string Location { get; set; }

        public string Sex { get; set; }

        public string PetStatus { get; set; }

        public string CreatedOn { get; set; }

        public string CoverPicturePath { get; set; }

        public bool IsPostCreator { get; set; }

        public bool IsPostLiked { get; set; }

        public string Category { get; set; }

        public string CreatorNickname { get; set; }

        public IEnumerable<PetProfilePicViewModel> Pictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetPost, PetProfileViewModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Pictures, opt => opt.MapFrom(x => x.PostPictures))
                .ForMember(x => x.PostId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.PetStatus, opt => opt.MapFrom(x => EnumHelper<PetStatus>.GetDisplayValue(x.PetStatus)))
                .ForMember(x => x.Sex, opt => opt.MapFrom(x => EnumHelper<Sex>.GetDisplayValue(x.Sex)))
                .ForMember(x => x.CreatorNickname, opt => opt.MapFrom(x => x.User.Nickname))
                .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                            x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                           x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
