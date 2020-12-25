namespace AnimalShelter.Web.ViewModels.StoriesModels
{
    using System;
    using System.Collections.Generic;

    public class StoryListViewModel
    {
        public int ItemsPerPage { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PagesCount => (int)Math.Ceiling((double)this.StoryCount / this.ItemsPerPage);

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public IEnumerable<StoryInListViewModel> Stories { get; set; }

        public int StoryCount { get; set; }
    }
}
