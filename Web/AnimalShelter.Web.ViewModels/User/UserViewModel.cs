namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AutoMapper;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Display(Name = "Описание...(до 1000 символа)")]
        public string Description { get; set; }

        public string CreatedOn { get; set; }

        [Display(Name = "Потребителско име")]
        public string Nickname { get; set; }

        [Display(Name = "Години")]
        public int Age { get; set; }

        [Display(Name = "Местоживеене")]
        public string Living { get; set; }

        public UserSex Sex { get; set; }

        public string CoverPicturePath { get; set; }

        public virtual IEnumerable<UserProfilePicViewModel> UserPictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
               .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
               .ForMember(x => x.UserPictures, opt => opt.MapFrom(x => x.UserPictures))
               .ForMember(x => x.Living, opt => opt.MapFrom(x => x.Living != null ? x.Living : "Не е въведено местоживеене"))
               .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                           x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                          x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
