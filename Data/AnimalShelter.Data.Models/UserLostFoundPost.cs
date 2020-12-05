using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class UserLostFoundPost : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int LostFoundPostId { get; set; }

        public PetLostAndFoundPost LostFoundPost { get; set; }
    }
}
