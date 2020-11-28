namespace AnimalShelter.Web.ViewModels.Adopt
{
    using System;
    using System.Collections.Generic;

    public class PetListViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PagesCount => (int)Math.Ceiling((double)this.DogCount / this.ItemsPerPage);

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public IEnumerable<PetInListViewModel> Dogs { get; set; }

        public int DogCount { get; set; }

    }
}
