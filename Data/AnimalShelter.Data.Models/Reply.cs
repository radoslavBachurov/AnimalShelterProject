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

        public string PostCreatorId { get; set; }

        public string RepliedToUserId { get; set; }

        public int PostId { get; set; }

        public virtual PetPost Post { get; set; }

        public bool IsReplyToComment { get; set; }
    }
}
