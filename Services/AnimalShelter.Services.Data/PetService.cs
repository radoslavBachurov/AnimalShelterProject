using AnimalShelter.Data.Common.Repositories;
using AnimalShelter.Data.Models;
using AnimalShelter.Services.Mapping;
using AnimalShelter.Web.ViewModels.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalShelter.Services.Data
{
    public class PetService : IPetService
    {
        private readonly IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository;

        public PetService(IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository)
        {
            this.adoptionPostsRepository = adoptionPostsRepository;
        }

        public PetProfileViewModel GetPetProfile(int postId)
        {
            var petProfile = this.adoptionPostsRepository.AllAsNoTracking()
                 .Where(x => x.Id == postId)
                 .To<PetProfileViewModel>()
                 .FirstOrDefault();

            return petProfile;
        }
    }
}
