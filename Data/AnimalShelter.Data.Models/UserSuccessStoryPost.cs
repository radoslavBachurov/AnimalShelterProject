using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class UserSuccessStoryPost : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int SuccessStoryId { get; set; }

        public SuccessStory SuccessStory { get; set; }
    }
}
