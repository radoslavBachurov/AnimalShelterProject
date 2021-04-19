namespace AnimalShelter.Web.Areas.Administration.Controllers
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Data;
    using AnimalShelter.Services.Data.AdministrationServices;
    using AnimalShelter.Web.ViewModels.Administration.ApprovalPost;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ApprovalPostController : AdministrationController
    {
        private readonly IApprovalPostService approvalService;
        private readonly IGetCountService countService;
        private readonly UserManager<ApplicationUser> userManager;

        public ApprovalPostController(
            IApprovalPostService approvalService,
            UserManager<ApplicationUser> userManager,
            IGetCountService countService)
        {
            this.approvalService = approvalService;
            this.userManager = userManager;
            this.countService = countService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult AdoptForApproval(int page = 1)
        {
            const int itemsPerPage = 5;

            var viewModel = new PetApprovalListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                Area = "Administration",
                Action = "AdoptForApproval",
            };

            viewModel.Count = this.countService.GetAllAdoptPostsForApprovalCount();
            viewModel.PetPosts = this.approvalService.GetAllAdoptPostsForApproval<PetApprovalViewModel>(page, itemsPerPage);

            return this.View(viewModel);
        }

        public IActionResult LostFoundForApproval()
        {
            return this.View();
        }

        public IActionResult StoriesForApproval()
        {
            return this.View();
        }
    }
}
