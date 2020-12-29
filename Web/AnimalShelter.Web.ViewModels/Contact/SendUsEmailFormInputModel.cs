namespace AnimalShelter.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class SendUsEmailFormInputModel
    {
        [Display(Name = "Тема")]
        [Required(ErrorMessage = "Темата е задължително поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Темата трябва да е от 2 до 20 символа")]
        public string About { get; set; }

        [Required(ErrorMessage = "Съобщението е задължително поле")]
        [Display(Name = "Твоето съобщение")]
        [DataType(DataType.MultilineText)]
        [StringLength(2500, MinimumLength = 10, ErrorMessage = "Съобщението трябва да е от 10 до 2500 символа")]
        public string Message { get; set; }
    }
}
