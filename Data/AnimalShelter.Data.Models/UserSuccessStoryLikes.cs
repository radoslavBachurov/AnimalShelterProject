namespace AnimalShelter.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using AnimalShelter.Data.Common.Models;

    public class UserSuccessStoryLikes : BaseDeletableModel<int>
    {
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int SuccessStoryId { get; set; }

        [ForeignKey("SuccessStoryId")]
        public SuccessStory SuccessStory { get; set; }
    }
}
