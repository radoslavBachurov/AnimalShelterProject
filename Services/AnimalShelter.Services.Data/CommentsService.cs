namespace AnimalShelter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Reply> replyPostsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<PetPost> postRepository;

        public CommentsService(
            IDeletableEntityRepository<Reply> replyPostsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<PetPost> postRepository)
        {
            this.replyPostsRepository = replyPostsRepository;
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        public async Task CreateAsync(int postId, ApplicationUser currentUser, string text, string answerToNickName, int? parentId = null)
        {
            var postCreatorUserId = this.postRepository.All().Where(x => x.Id == postId).FirstOrDefault().UserId;
            var postCreatorUser = this.userRepository.All().Where(x => x.Id == postCreatorUserId).FirstOrDefault();

            var answeredToCommentUser = this.userRepository.All().Where(x => x.Nickname == answerToNickName).FirstOrDefault();

            var newReplytoCreator = new Reply()
            {
                PostId = postId,
                UserId = currentUser.Id,
                IsReplyToComment = answerToNickName != null ? true : false,
                ParentId = parentId,
                Text = text,
                PostCreatorId = postCreatorUserId != currentUser.Id ? postCreatorUserId : null,
                RepliedToUserId = answeredToCommentUser != null ? answeredToCommentUser.Id : null,
            };

            if (answeredToCommentUser != null)
            {
                if (postCreatorUserId == answeredToCommentUser.Id)
                {
                    postCreatorUser.AnswerCounter++;
                }
                else
                {
                    if (postCreatorUserId != currentUser.Id)
                    {
                        postCreatorUser.AnswerCounter++;
                    }

                    answeredToCommentUser.AnswerCounter++;
                }
            }
            else if (postCreatorUserId != currentUser.Id)
            {
                postCreatorUser.AnswerCounter++;
            }

            await this.replyPostsRepository.AddAsync(newReplytoCreator);

            await this.replyPostsRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }

        public bool IsInThisPostId(int postId, int commentId)
        {
            var result = this.replyPostsRepository.AllAsNoTracking()
                .Where(x => x.Id == commentId)
                .Select(x => x.PostId)
                .FirstOrDefault();

            return result == postId ? true : false;
        }
    }
}
