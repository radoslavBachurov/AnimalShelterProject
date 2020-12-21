using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelter.Common;
using AnimalShelter.Data.Models;
using AnimalShelter.Services.Data;
using AnimalShelter.Web.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Web.Controllers.ApiControllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IGetCountService getCountService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserInfoController(IGetCountService getCountService, UserManager<ApplicationUser> userManager)
        {
            this.getCountService = getCountService;
            this.userManager = userManager;
        }

        [HttpPost("info")]
        public ActionResult<string> GetPictures()
        {
            return string.Empty;
        }

        [HttpPost("images")]
        public async Task<IActionResult> GetPictures([FromForm] UserImageInputModel uploadPhotos)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            int countPhotos = this.getCountService.GetCurrentUserPhotosCount(user.Id);
            int countPhotosToUpload = uploadPhotos.Images?.ToList().Count() ?? 0;

            if ((countPhotos + countPhotosToUpload) > GlobalConstants.MaxUserPhotosUserCanUpload)
            {
                string photosLeft = GlobalConstants.MaxPostPhotosUserCanUpload - countPhotos > 0 ? $"Остават ви {GlobalConstants.MaxUserPhotosUserCanUpload - countPhotos }" : "Не можете да качите повече снимки";

                this.ModelState.AddModelError("Images", $"Можете да качите максимум 100 снимки.{photosLeft}");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            //to change
            return this.CreatedAtRoute("DefaultApi", new { id = "333" });
        }
    }
}
