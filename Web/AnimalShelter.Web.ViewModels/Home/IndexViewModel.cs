namespace AnimalShelter.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.HappyStories = new List<HappyEndingsIndexViewModel>();
        }

        public int DogsCount { get; set; }

        public int CatCount { get; set; }

        public int OtherAnimalsCount { get; set; }

        public int AllAnimals => this.DogsCount + this.CatCount + this.OtherAnimalsCount;

        public int AdoptedAnimals { get; set; }

        public int Volunteers { get; set; }

        public List<HappyEndingsIndexViewModel> HappyStories { get; set; }
    }
}
