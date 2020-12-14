namespace AnimalShelter.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Data.Models.Enums;

    public class UserInfoInputModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        [MinLength(4, ErrorMessage = "Потребителското име трябва да е между 4 и 15 символа")]
        [MaxLength(15, ErrorMessage = "Потребителското име трябва да е между 4 и 15 символа")]
        public string Nickname { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Годините трябва да са между 1 и 100")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Местожителство")]
        [MinLength(2, ErrorMessage = "Името трябва да е между 2 и 30 символа")]
        [MaxLength(30, ErrorMessage = "Името трябва да е между 2 и 30 символа")]
        public string Living { get; set; }

        [Required]
        public Sex Sex { get; set; }
    }
}
