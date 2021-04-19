using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class SuccessStory : BaseDeletableModel<int>
    {
        public SuccessStory()
        {
            this.PostPictures = new HashSet<Picture>();
            this.UserLikes = new HashSet<UserSuccessStoryLikes>();
        }

        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PetName { get; set; }

        public int Likes { get; set; }

        public bool IsApproved { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<UserSuccessStoryLikes> UserLikes { get; set; }
    }
}
