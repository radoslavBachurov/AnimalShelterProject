namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using AnimalShelter.Common;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string CreatedOn { get; set; }

        public string Nickname { get; set; }

        public string Age { get; set; }

        public string Living { get; set; }

        public string Sex { get; set; }

        public string CoverPicturePath { get; set; }

        public virtual IEnumerable<UserProfilePicViewModel> UserPictures { get; set; }

        // For input model

        [Display(Name = "Описание...(до 1000 символа)")]
        [StringLength(1000, ErrorMessage = "Името трябва до 1000 символа")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Името е задължително поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 20 символа")]
        [Display(Name = "Username")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Името не трябва да съдържа празни полета")]
        public string InputNickname { get; set; }

        [Range(1, 100, ErrorMessage = "Годините трябва да са между 1 и 100")]
        public int InputAge { get; set; }

        [Display(Name = "Местожителство")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 30 символа")]
        public string InputLiving { get; set; }

        public UserSex InputSex { get; set; }

        [ImageValidationAttribute(15 * 1024 * 1024, GlobalConstants.MaxUserPhotosUserCanUpload, FilesTooManyMessage = "Sorry but you can upload maximum of 500 images")]
        public IEnumerable<IFormFile> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
               .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
               .ForMember(x => x.UserPictures, opt => opt.MapFrom(x => x.UserPictures))
               .ForMember(x => x.Sex, opt => opt.MapFrom(x => x.Sex != 0 ? EnumHelper<UserSex>.GetDisplayValue(x.Sex) : "Не е въведен"))
               .ForMember(x => x.InputSex, opt => opt.MapFrom(x => x.Sex != 0 ? x.Sex : UserSex.Male))
               .ForMember(x => x.Age, opt => opt.MapFrom(x => x.Age != 0 ? x.Age.ToString() : "Не е въведен"))
               .ForMember(x => x.InputAge, opt => opt.MapFrom(x => x.Age != 0 ? x.Age : 0))
               .ForMember(x => x.Living, opt => opt.MapFrom(x => x.Living != null ? x.Living : "Не е въведен"))
               .ForMember(x => x.InputLiving, opt => opt.MapFrom(x => x.Living != null ? x.Living : "Не е въведен"))
               .ForMember(x => x.InputNickname, opt => opt.MapFrom(x => x.Nickname))
               .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                           x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                          x.UserPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}