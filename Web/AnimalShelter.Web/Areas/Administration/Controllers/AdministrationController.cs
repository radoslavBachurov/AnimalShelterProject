namespace AnimalShelter.Web.Areas.Administration.Controllers
{
    using AnimalShelter.Common;
    using AnimalShelter.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator,Moderator")]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
