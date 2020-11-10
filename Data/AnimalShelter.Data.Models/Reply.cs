using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class Reply : BaseDeletableModel<string>
    {
        public Reply()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ReplyPictures = new HashSet<Picture>();
        }

        public string Text { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Picture> ReplyPictures { get; set; }
    }
}
