using AnimalShelter.Web.ViewModels.Pet;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Services.Data
{
    public interface IPetService
    {
        PetProfileViewModel GetPetProfile(int postId);
    }
}
