using AnimalShelter.Web.ViewModels.Adopt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelter.Web.Controllers
{
    public class AdoptController : BaseController
    {
        public AdoptController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult SearchResults()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreatePetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
