using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Web.Controllers
{
    public class PetController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
