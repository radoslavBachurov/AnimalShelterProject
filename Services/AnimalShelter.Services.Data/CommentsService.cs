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
            IDeletableEntityRepository<Answer> answerPostsRepository,
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
            var replyToPostCreator = this.userRepository.All().Where(x => x.Id == postCreatorUserId).FirstOrDefault();

            if (replyToPostCreator.Id != currentUser.Id)
            {
                var newAnswertoCreator = new Answer()
                {
                    PostId = postId,
                    AnswerToId = replyToPostCreator.Id,
                    ReplyToComment = false,
                };

                replyToPostCreator.Answers.Add(newAnswertoCreator);
                replyToPostCreator.AnswerCounter++;
            }

            if (answerToNickName != null)
            {
                var answeredToCommentUser = this.userRepository.All().Where(x => x.Nickname == answerToNickName).FirstOrDefault();

                var newAnswertoComment = new Answer()
                {
                    PostId = postId,
                    AnswerToId = answeredToCommentUser.Id,
                    ReplyToComment = true,
                };

                answeredToCommentUser.Answers.Add(newAnswertoComment);
                answeredToCommentUser.AnswerCounter++;
            }

            var newReply = new Reply()
            {
                PetPostId = postId,
                Text = text,
                UserId = currentUser.Id,
                ParentId = parentId,
            };

            await this.replyPostsRepository.AddAsync(newReply);
            await this.replyPostsRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }

        public bool IsInThisPostId(int postId, int commentId)
        {
            var result = this.replyPostsRepository.AllAsNoTracking()
                .Where(x => x.Id == commentId)
                .Select(x => x.PetPostId)
                .FirstOrDefault();

            return result == postId ? true : false;
        }
    }
}
