namespace AnimalShelter.Data.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using AnimalShelter.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        public Reply()
        {
            this.ReplyPictures = new HashSet<Picture>();
        }

        public string Text { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? PetAdoptionPostId { get; set; }

        public PetAdoptionPost PetAdoptionPost { get; set; }

        public int? PetLostAndFoundPostId { get; set; }

        public PetLostAndFoundPost PetLostAndFound { get; set; }

        public virtual ICollection<Picture> ReplyPictures { get; set; }
    }
}
