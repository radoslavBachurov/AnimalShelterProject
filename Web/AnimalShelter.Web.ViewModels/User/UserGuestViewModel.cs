namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AutoMapper;

    public class UserGuestViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string CreatedOn { get; set; }

        public string Nickname { get; set; }

        public string Age { get; set; }

        public string Living { get; set; }

        public string Sex { get; set; }

        public string Description { get; set; }

        public string CoverPicturePath { get; set; }

        public virtual IEnumerable<UserProfilePicViewModel> UserPictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserGuestViewModel>()
               .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
               .ForMember(x => x.UserPictures, opt => opt.MapFrom(x => x.UserPictures))
               .ForMember(x => x.Sex, opt => opt.MapFrom(x => x.Sex != 0 ? EnumHelper<UserSex>.GetDisplayValue(x.Sex) : "Не е въведен"))
               .ForMember(x => x.Age, opt => opt.MapFrom(x => x.Age != 0 ? x.Age.ToString() : "Не е въведен"))
               .ForMember(x => x.Living, opt => opt.MapFrom(x => x.Living != null ? x.Living : "Не е въведен"))
               .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                           x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                          x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
