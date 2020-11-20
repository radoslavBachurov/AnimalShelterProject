namespace AnimalShelter.Data.Models
{
    using AnimalShelter.Data.Common.Models;
    using AnimalShelter.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PetAdoptionPost : BaseDeletableModel<int>
    {
        public PetAdoptionPost()
        {
            this.PostPictures = new HashSet<Picture>();
            this.Replies = new HashSet<Reply>();
        }

        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int Likes { get; set; }

        public City Location { get; set; }

        public Sex Sex { get; set; }

        public TypePet Type { get; set; }

        public bool IsAdopted { get; set; }

        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
