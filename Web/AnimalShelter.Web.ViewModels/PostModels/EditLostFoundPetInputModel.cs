namespace AnimalShelter.Web.ViewModels.PostModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [MaxLength(3000)]
        public string Description { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(12)]
        public string Name { get; set; }

        [Required]
        public City Location { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public TypePet Type { get; set; }

        [Required]
        public LostFoundCreateOptions Status { get; set; }

        [ImageValidationAttribute(15 * 1024 * 1024)]
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
