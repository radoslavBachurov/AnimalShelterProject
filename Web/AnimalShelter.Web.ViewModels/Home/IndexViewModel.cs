using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Web.ViewModels.Home
{
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

        public ICollection<HappyEndingsIndexViewModel> HappyStories { get; set; }
    }
}
