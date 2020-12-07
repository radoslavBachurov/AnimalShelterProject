namespace AnimalShelter.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SearchTypePet
    {
        [Display(Name = "Всички")]
        All = 1,
        [Display(Name = "Куче")]
        Dog = 2,
        [Display(Name = "Котка")]
        Cat = 3,
        [Display(Name = "Други")]
        Other = 4,
    }
}
