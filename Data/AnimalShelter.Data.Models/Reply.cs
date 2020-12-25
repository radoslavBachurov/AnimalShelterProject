namespace AnimalShelter.Data.Models
{
    using AnimalShelter.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? PetPostId { get; set; }

        public PetPost PetPost { get; set; }
    }
}
