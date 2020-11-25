using AnimalShelter.Data.Models;
using AnimalShelter.Web.ViewModels.Adopt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Services.Data
{
    public interface IAdoptService
    {
        Task CreateAdoptionPost(CreatePetInputModel input, ApplicationUser user, string webRoot);
    }
}
