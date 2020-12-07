namespace AnimalShelter.Web.ViewModels.SearchResults
{
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Data.Models.Enums;

    public class SearchResultsInputModel
    {
        [Required]
        public SearchCity Location { get; set; }

        [Required]
        public SearchSex Sex { get; set; }

        [Required]
        public SearchTypePet Type { get; set; }

        [Required]
        public SearchPetStatus PetStatus { get; set; }
    }
}
