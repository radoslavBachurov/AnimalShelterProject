namespace AnimalShelter.Web.ViewModels.PostModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class CreateAdoptPetInputModel : IMapTo<PetPost>
    {
        [Required]
        [Display(Name = "Описание (Точно местоположение,телефон,състояние и др...)")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [MaxLength(3000)]
        public string Description { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(12)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        public City Location { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public TypePet Type { get; set; }

        [Required]
        [ImageValidationAttribute(15 * 1024 * 1024)]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
