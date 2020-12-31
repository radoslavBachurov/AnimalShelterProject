namespace AnimalShelter.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class NotificationViewComponentModel : IMapFrom<ApplicationUser>
    {
        public int AnswerCounter { get; set; }

        public List<RepliesViewComponentModel> Notifications { get; set; }
    }
}
