namespace AnimalShelter.Services.Data.AdministrationServices
{
    using System.Collections.Generic;

    public interface IApprovalPostService
    {
        List<T> GetAllAdoptPostsForApproval<T>(int page,int itemsPerPage);

        List<T> GetAllLostFoundPostsForApproval<T>(int page, int itemsPerPage);

        List<T> GetAllStoriesPostsForApproval<T>(int page, int itemsPerPage);
    }
}
