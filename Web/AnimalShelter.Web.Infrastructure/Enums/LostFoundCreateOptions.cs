using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Web.Infrastructure.Enums
{
    public enum LostFoundCreateOptions
    {
        [Display(Name = "Изгубени домашни любимци")] Lost = 1,

        [Display(Name = "Намерени домашни любимци")] Found = 2,
    }
}
