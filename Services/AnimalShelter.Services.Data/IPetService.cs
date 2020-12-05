using AnimalShelter.Data.Models;
using AnimalShelter.Web.ViewModels.Pet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Services.Data
{
    public interface IPetService
    {
        PetProfileViewModel GetPetProfile(int postId, ApplicationUser user);

        Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input,ApplicationUser user);
    }
}
