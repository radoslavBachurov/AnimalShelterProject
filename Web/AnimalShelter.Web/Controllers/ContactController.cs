namespace AnimalShelter.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.Infrastructure.EmailSender;
    using AnimalShelter.Web.Infrastructure.EmailSender.EmailViewModels;
    using AnimalShelter.Web.ViewModels.Contact;
    using AnimalShelter.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public ContactController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [Authorize]
        public IActionResult SendEmailToUs()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendEmailToUs(SendUsEmailFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            var mailToUs = new EmailSendToUsModel()
            {
                SenderAge = user.Age.ToString(),
                SenderLiving = user.Living,
                SenderSex=user.Sex.ToString(),
                SenderEmail=user.Email,
                SenderUsername = user.Nickname,
                Subject = input.About,
                Message = input.Message,
                SingleDate = DateTime.UtcNow,
            };

            await this.emailSender.SendEmailToUsAsync(mailToUs);

            this.TempData["Message"] = $"Вашето съобщение беше изпратено успешно.";
            return this.RedirectToAction(nameof(this.SendEmailToUs));
        }

        // Using SendGrid
        // [HttpPost]
        // [Authorize]
        // public async Task<IActionResult> SendEmailToUs(SendUsEmailFormInputModel input)
        // {
        //     if (!this.ModelState.IsValid)
        //     {
        //         return this.View(input);
        //     }

        //     var user = await this.userManager.GetUserAsync(this.User);
        //     input.Email = user.Email;

        //     await this.emailSender.SendEmailAsync(
        //         input.Email,
        //         input.FirstName + " " + input.SecondName,
        //         GlobalConstants.SystemEmail,
        //         input.About,
        //         input.Message);

        //     this.TempData["Message"] = $"Вашето съобщение беше изпратено успешно.";
        //     return this.RedirectToAction(nameof(this.SendEmailToUs));
        // }
    }
}
