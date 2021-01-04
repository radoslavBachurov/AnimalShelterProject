namespace AnimalShelter.Web.ViewModels.PostModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AnimalShelter.Common;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure;
    using AnimalShelter.Web.Infrastructure.Enums;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    public class EditLostFoundPetInputModel : IMapFrom<PetPost>, IMapTo<PetPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Описанието е задължително поле")]
        [Display(Name = "Описание (Точно местоположение,телефон,състояние и др...Максимум 6000 символа)")]
        [DataType(DataType.MultilineText)]
        [StringLength(GlobalConstants.MaxCharactersInPostDescription, MinimumLength = 10, ErrorMessage = "Описанието трябва да е от 10 до 6000 символа")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Името е задължително поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 20 символа")]
        [Display(Name = "Име")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Името не трябва да съдържа празни полета")]
        public string Name { get; set; }

        [Required]
        public City Location { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public TypePet Type { get; set; }

        [Required]
        public LostFoundCreateOptions Status { get; set; }

        [ImageValidationAttribute(GlobalConstants.MaximumSizeOfOnePicture, GlobalConstants.MaxPostPhotosUserCanUpload)]
        public IEnumerable<IFormFile> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetPost, EditLostFoundPetInputModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => EnumHelper<LostFoundCreateOptions>.ParseEnum(x.PetStatus.ToString())));

            configuration.CreateMap<EditLostFoundPetInputModel, PetPost>()
               .ForMember(x => x.PetStatus, opt => opt.MapFrom(x => EnumHelper<PetStatus>.ParseEnum(x.Status.ToString())));
        }
    }
}
