using AnimalShelter.Data.Models;
using AnimalShelter.Services.Data;
using AnimalShelter.Web.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalShelter.Web.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentsService commentService;

        public CommentsController(UserManager<ApplicationUser> userManager, ICommentsService commentService)
        {
            this.userManager = userManager;
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReplyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("PetProfile", "Pet", new { id = input.PostId });
            }

            var parentId = input.ParentId == 0 ? null : input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentService.IsInThisPostId(input.PostId, parentId.Value))
                {
                    return this.BadRequest();
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.commentService.CreateAsync(input.PostId, user, input.Text, input.AnswerTo, parentId);

            return this.RedirectToAction("PetProfile", "Pet", new { id = input.PostId });
        }
    }
}
