namespace AnimalShelter.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Data.Models.Enums;

    public class UserInfoInputModel
    {
        [Required(ErrorMessage = "Името е задължително поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Името трябва да е от 2 до 20 символа")]
        [Display(Name = "Username")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Името не трябва да съдържа празни полета")]
        public string Nickname { get; set; }

        [Display(Name = "Описание...(до 1000 символа)")]
        [StringLength(1000, ErrorMessage = "Описанието трябва да е до 1000 символа")]
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "Годините трябва да са между 1 и 100")]
        public int Age { get; set; }

        [Display(Name = "Местожителство")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името за местожителство трябва да е от 2 до 30 символа")]
        public string Living { get; set; }

        public int Sex { get; set; }
    }
}
