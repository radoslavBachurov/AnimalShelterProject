namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AnimalShelter.Common;
    using AnimalShelter.Data.Models.Enums;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class UserInfoInputModel
    {
        [Required(ErrorMessage = "Името е задължително поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 20 символа")]
        [Display(Name = "Username")]
        public string Nickname { get; set; }

        [Range(1, 100, ErrorMessage = "Годините трябва да са между 1 и 100")]
        public int Age { get; set; }

        [Display(Name = "Местожителство")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 30 символа")]
        public string Living { get; set; }

        public Sex Sex { get; set; }
    }
}
