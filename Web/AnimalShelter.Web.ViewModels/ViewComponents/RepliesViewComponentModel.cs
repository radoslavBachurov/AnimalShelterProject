namespace AnimalShelter.Web.ViewModels.ViewComponents
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class RepliesViewComponentModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string PostName { get; set; }

        public bool ReplyToComment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, RepliesViewComponentModel>()
            .ForMember(x => x.PostName, opt => opt.MapFrom(x => x.PetPost.Name));
        }
    }
}
