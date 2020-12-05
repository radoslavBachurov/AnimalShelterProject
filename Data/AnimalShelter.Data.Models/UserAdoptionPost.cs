using AnimalShelter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelter.Data.Models
{
    public class UserAdoptionPost : BaseDeletableModel<int>
    {
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int PetAdoptionPostId { get; set; }

        [ForeignKey("PetAdoptionPostId")]
        public PetAdoptionPost PetAdoptionPost { get; set; }
    }
}
