using AnimalShelter.Data.Common.Models;
using AnimalShelter.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class PetLostAndFoundPost : BaseDeletableModel<string>
    {
        public PetLostAndFoundPost()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PostPictures = new HashSet<Picture>();
            this.Replies = new HashSet<Reply>();
        }

        public string Title { get; set; }

        public string Text { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public City Location { get; set; }

        public Sex Sex { get; set; }

        public TypePet Type { get; set; }

        public PetStatus PetStatus { get; set; }

        public int Likes { get; set; }

        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
