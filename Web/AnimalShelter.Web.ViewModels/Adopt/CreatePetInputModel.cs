using AnimalShelter.Data.Models;
using AnimalShelter.Data.Models.Enums;
using AnimalShelter.Web.Infrastructure.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Web.ViewModels.Adopt
{
    public class CreatePetInputModel
    {
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [MaxLength(3000)]
        public string Description { get; set; }

        public string UserId { get; set; }

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
