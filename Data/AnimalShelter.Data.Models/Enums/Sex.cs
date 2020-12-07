using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Data.Models.Enums
{
    public enum Sex
    {
        [Display(Name = "Мъжко")]
        Male = 2,
        [Display(Name = "Женско")]
        Female = 3,
    }
}
