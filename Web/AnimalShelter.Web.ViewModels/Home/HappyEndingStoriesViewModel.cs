using AnimalShelter.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Web.ViewModels.Home
{
    public class HappyEndingsIndexViewModel
    {
        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PetName { get; set; }

        public int Likes { get; set; }

        public Picture Avatar { get; set; }

        public string CreatedOn { get; set; }
    }
}
