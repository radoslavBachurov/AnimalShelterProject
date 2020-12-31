namespace AnimalShelter.Web.ViewModels.ViewComponents
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class RepliesViewComponentModel : IMapFrom<Reply>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string PostName { get; set; }

        public bool IsReplyToComment { get; set; }

        public string ReplyCreator { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, RepliesViewComponentModel>()
            .ForMember(x => x.PostName, opt => opt.MapFrom(x => x.Post.Name))
            .ForMember(x => x.ReplyCreator, opt => opt.MapFrom(x => x.User.Nickname));
        }
    }
}
