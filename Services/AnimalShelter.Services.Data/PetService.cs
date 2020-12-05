using AnimalShelter.Data.Common.Repositories;
using AnimalShelter.Data.Models;
using AnimalShelter.Services.Mapping;
using AnimalShelter.Web.ViewModels.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Services.Data
{
    public class PetService : IPetService
    {
        private readonly IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository;
        private readonly IDeletableEntityRepository<UserAdoptionPost> userAdoptionPostsRepository;

        public PetService(
            IDeletableEntityRepository<PetAdoptionPost> adoptionPostsRepository,
            IDeletableEntityRepository<UserAdoptionPost> userAdoptionPostsRepository)
        {
            this.adoptionPostsRepository = adoptionPostsRepository;
            this.userAdoptionPostsRepository = userAdoptionPostsRepository;
        }

        public PetProfileViewModel GetPetProfile(int postId, ApplicationUser user)
        {
            var userLikedThisPost = this.userAdoptionPostsRepository.AllAsNoTracking()
                .Where(x => x.ApplicationUserId == user.Id && x.PetAdoptionPostId == postId).ToList();

            var petProfile = this.adoptionPostsRepository.AllAsNoTracking()
                 .Where(x => x.Id == postId && x.IsApproved == true)
                 .To<PetProfileViewModel>()
                 .FirstOrDefault();

            petProfile.CurrentUserId = user.Id;
            petProfile.IsPostLiked = userLikedThisPost.Any() ? true : false;

            return petProfile;
        }

        public async Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input, ApplicationUser user)
        {
            var outputModel = new LikeOutputModel();
            var post = this.adoptionPostsRepository.All().Where(x => x.Id == input.PostId && x.IsApproved).FirstOrDefault();

            if (post != null)
            {
                if (input.IsLiked)
                {
                    post.Likes--;
                    var like = this.userAdoptionPostsRepository.AllAsNoTracking().Where(x => x.ApplicationUserId == user.Id && x.PetAdoptionPostId == input.PostId).FirstOrDefault();
                    this.userAdoptionPostsRepository.Delete(like);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = false;

                    await this.adoptionPostsRepository.SaveChangesAsync();
                    await this.userAdoptionPostsRepository.SaveChangesAsync();

                    return outputModel;
                }
                else
                {
                    var userLikes = new UserAdoptionPost() { PetAdoptionPostId = post.Id, ApplicationUserId = user.Id };
                    post.Likes++;
                    await this.userAdoptionPostsRepository.AddAsync(userLikes);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = true;

                    await this.adoptionPostsRepository.SaveChangesAsync();
                    await this.userAdoptionPostsRepository.SaveChangesAsync();

                    return outputModel;
                }
            }

            outputModel.Likes = -1;
            return outputModel;
        }
    }
}
