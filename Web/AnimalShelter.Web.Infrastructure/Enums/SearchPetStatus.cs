namespace AnimalShelter.Web.Infrastructure.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SearchPetStatus
    {
        [Display(Name = "Всички")] All = 1,
        [Display(Name = "За Осиновяване")] ForAdoption = 2,
        [Display(Name = "Осиновен")] Adopted = 3,
        [Display(Name = "Изгубени домашни любимци")] Lost = 4,
        [Display(Name = "Намерени домашни любимци")] Found = 5,
        [Display(Name = "Намерени и изгубени върнати вкъщи")] LostFoundBackInHome = 6,
    }
}
