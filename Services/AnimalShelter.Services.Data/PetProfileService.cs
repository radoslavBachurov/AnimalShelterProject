namespace AnimalShelter.Services.Data
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.Pet;

    public class PetProfileService : IPetProfileService
    {
        private readonly IDeletableEntityRepository<PetPost> petPostsRepository;
        private readonly IDeletableEntityRepository<UserPetPost> userPetPostsRepository;

        public PetProfileService(
            IDeletableEntityRepository<PetPost> petPostsRepository,
            IDeletableEntityRepository<UserPetPost> userPetPostsRepository)
        {
            this.petPostsRepository = petPostsRepository;
            this.userPetPostsRepository = userPetPostsRepository;
        }

        public PetProfileViewModel GetPetProfile(int postId, ApplicationUser user)
        {
            var userLikedThisPost = new List<UserPetPost>();
            if (user != null)
            {
                userLikedThisPost = this.userPetPostsRepository.AllAsNoTracking()
               .Where(x => x.ApplicationUserId == user.Id && x.PetPostId == postId).ToList();
            }

            var petProfile = this.petPostsRepository.AllAsNoTracking()
                          .Where(x => x.Id == postId && x.IsApproved == true)
                          .To<PetProfileViewModel>()
                          .FirstOrDefault();

            petProfile.CurrentUserId = user != null ? user.Id : null;
            petProfile.IsPostLiked = userLikedThisPost.Any() ? true : false;

            return petProfile;
        }

        public async Task<LikeOutputModel> AddRemoveLikeToPostAsync(LikeInputModel input, ApplicationUser user)
        {
            var outputModel = new LikeOutputModel();
            var post = this.petPostsRepository.All().Where(x => x.Id == input.PostId && x.IsApproved).FirstOrDefault();

            if (post != null)
            {
                if (input.IsLiked)
                {
                    post.Likes--;
                    var like = this.userPetPostsRepository.AllAsNoTracking().Where(x => x.ApplicationUserId == user.Id && x.PetPostId == input.PostId).FirstOrDefault();
                    this.userPetPostsRepository.HardDelete(like);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = false;

                    await this.petPostsRepository.SaveChangesAsync();
                    await this.userPetPostsRepository.SaveChangesAsync();

                    return outputModel;
                }
                else
                {
                    var userLikes = new UserPetPost() { PetPostId = post.Id, ApplicationUserId = user.Id };
                    post.Likes++;
                    await this.userPetPostsRepository.AddAsync(userLikes);
                    outputModel.Likes = post.Likes;
                    outputModel.IsLiked = true;

                    await this.petPostsRepository.SaveChangesAsync();
                    await this.userPetPostsRepository.SaveChangesAsync();

                    return outputModel;
                }
            }

            outputModel.Likes = -1;
            return outputModel;
        }
    }
}
