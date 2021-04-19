namespace AnimalShelter.Web.ViewModels.Administration.ApprovalPost
{
    using System.Collections.Generic;

    public class PetApprovalListViewModel : PagingViewModel
    {
        public IEnumerable<PetApprovalViewModel> PetPosts { get; set; }
    }
}
