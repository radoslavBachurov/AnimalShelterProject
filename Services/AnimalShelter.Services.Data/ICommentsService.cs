namespace AnimalShelter.Services.Data
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;

    public interface ICommentsService
    {
        Task CreateAsync(int postId, ApplicationUser currentUser, string text, string answerToNickName, int? parentId = null);

        bool IsInThisPostId(int postId, int commentId);
    }
}
