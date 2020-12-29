namespace AnimalShelter.Data.Models
{
    using AnimalShelter.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public int? ParentId { get; set; }

        public virtual Reply Parent { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PetPostId { get; set; }

        public virtual PetPost PetPost { get; set; }
    }
}
