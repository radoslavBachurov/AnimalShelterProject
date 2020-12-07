namespace AnimalShelter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AnimalShelter.Data.Common.Models;
    using AnimalShelter.Data.Models.Enums;

    public class PetPost : BaseDeletableModel<int>
    {
        public PetPost()
        {
            this.PostPictures = new HashSet<Picture>();
            this.Replies = new HashSet<Reply>();
            this.UserLikes = new HashSet<UserPetPost>();
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int Likes { get; set; }

        public City Location { get; set; }

        public Sex Sex { get; set; }

        public TypePet Type { get; set; }

        public PetStatus PetStatus { get; set; }

        public bool IsApproved { get; set; } = true;

        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<UserPetPost> UserLikes { get; set; }
    }
}
