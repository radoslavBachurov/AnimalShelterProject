namespace AnimalShelter.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using AnimalShelter.Data.Common.Models;

    public class UserPetPost : BaseDeletableModel<int>
    {
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int PetPostId { get; set; }

        [ForeignKey("PetPostId")]
        public PetPost PetPost { get; set; }
    }
}
