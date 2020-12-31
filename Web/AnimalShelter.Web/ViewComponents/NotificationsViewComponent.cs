namespace AnimalShelter.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Common.Repositories;
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Reply> replyRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationsViewComponent(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Reply> replyRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.replyRepository = replyRepository;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await this.userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)this.User);

            var newNotifications = new NotificationViewComponentModel();
            if (user != null)
            {
                newNotifications = this.userRepository.All()
                                                     .Where(x => x.Id == user.Id)
                                                     .To<NotificationViewComponentModel>()
                                                     .FirstOrDefault();

                newNotifications.Notifications = this.replyRepository.AllAsNoTracking()
                                                                     .Where(x => x.PostCreatorId == user.Id || x.RepliedToUserId == user.Id)
                                                                     .OrderByDescending(x => x.Id).Take(20).To<RepliesViewComponentModel>()
                                                                     .ToList();
            }

            return this.View(newNotifications);
        }
    }
}
