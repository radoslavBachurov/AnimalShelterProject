namespace AnimalShelter.Web.Controllers.ApiControllers
{
    using AnimalShelter.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserInfoController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

    }
}
