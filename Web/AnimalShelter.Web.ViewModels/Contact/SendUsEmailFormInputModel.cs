namespace AnimalShelter.Web.ViewModels.Contact
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SendUsEmailFormInputModel
    {
        [Required]
        [Display(Name = "Относно")]
        [MinLength(2)]
        [MaxLength(50)]
        public string About { get; set; }

        [Required]
        [Display(Name = "Твоето съобщение")]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [MaxLength(3000)]
        public string Message { get; set; }
    }
}
