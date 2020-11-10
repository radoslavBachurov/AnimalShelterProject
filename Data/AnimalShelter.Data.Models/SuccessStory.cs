using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class SuccessStory : BaseDeletableModel<string>
    {
        public SuccessStory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PostPictures = new HashSet<Picture>();
            this.Replies = new HashSet<Reply>();
        }

        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PetName { get; set; }

        public int Likes { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
