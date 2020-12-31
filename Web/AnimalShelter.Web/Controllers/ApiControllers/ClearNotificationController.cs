namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class ClearNotificationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public ClearNotificationController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userService.ClearNotificationsAsync(user.Id);

            return this.Ok("Success");
        }
    }
}
