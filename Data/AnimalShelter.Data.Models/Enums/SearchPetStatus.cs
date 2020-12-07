namespace AnimalShelter.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SearchPetStatus
    {
        [Display(Name = "Всички")]
        All = 1,
        [Display(Name = "За Осиновяване")] ForAdoption = 2,
        [Display(Name = "Осиновени")] Adopted = 3,
        [Display(Name = "Загубени домашни животни")] Lost = 4,
        [Display(Name = "Намерени домашни животни")] Found = 5,
        [Display(Name = "Намерени и загубени върнати вкъщи")] LostFoundBackInHome = 6,
    }
}
