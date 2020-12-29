namespace AnimalShelter.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class NotificationViewComponentModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public int AnswerCounter { get; set; }

        public List<RepliesViewComponentModel> Notifications { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, NotificationViewComponentModel>()
            .ForMember(x => x.Notifications, opt => opt.MapFrom(x => x.Answers.OrderByDescending(x => x.Id).Take(40)));
        }
    }
}
