namespace AnimalShelter.Web.ViewModels.Home
{
    using AnimalShelter.Data.Models;

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
