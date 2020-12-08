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

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string CreatedOn { get; set; }

        public string Nickname { get; set; }

        public string Age { get; set; } = "Не е въведен";

        public string Living { get; set; } = "Не е въведен";

        public string Sex { get; set; } = "Не е въведен";

        public string CoverPicturePath { get; set; }

        public virtual IEnumerable<UserProfilePicViewModel> UserPictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
               .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
               .ForMember(x => x.UserPictures, opt => opt.MapFrom(x => x.UserPictures))
               .ForMember(x => x.Sex, opt => opt.MapFrom(x => EnumHelper<Sex>.GetDisplayValue(x.Sex)))
               .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                           x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                          x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}