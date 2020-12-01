using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelter.Data.Models;
using AnimalShelter.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Web.Controllers
{
    public class PetController : BaseController
    {
        private readonly IPetService petService;
        private readonly UserManager<ApplicationUser> userManager;

        public PetController(IPetService petService, UserManager<ApplicationUser> userManager)
        {
            this.petService = petService;
            this.userManager = userManager;
        }

        public IActionResult Profile(int id,string name)
        {
            var postId = id;
            var viewModel = this.petService.GetPetProfile(postId);
            return this.View(viewModel);
        }
    }
}
