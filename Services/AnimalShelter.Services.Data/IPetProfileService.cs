namespace AnimalShelter.Services.Data
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Web.ViewModels.Pet;

    public interface IPetProfileService
    {
        PetProfileViewModel GetPetProfile(int postId, ApplicationUser user);

        Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input, ApplicationUser user);
    }
}
