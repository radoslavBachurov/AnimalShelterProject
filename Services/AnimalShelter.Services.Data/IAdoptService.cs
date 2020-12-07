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
        Task CreateAdoptionPostAsync(CreateAdoptPetInputModel input, ApplicationUser user, string webRoot);

        IEnumerable<T> GetAllAnimalsForAdoption<T>(int pageNumber, int itemsPerPage, string orderByProperty, string orderAscDesc);

        IEnumerable<T> GetAllAnimalsForAdoptionByType<T>(int pageNumber, int itemsPerPage,string typeAnimal, string orderByProperty, string orderAscDesc);
    }
}
