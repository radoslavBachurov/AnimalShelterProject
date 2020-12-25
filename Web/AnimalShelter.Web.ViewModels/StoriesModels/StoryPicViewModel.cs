namespace AnimalShelter.Web.ViewModels.StoriesModels
{
    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class StoryPicViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        public string Path { get; set; }

        public bool IsCoverPicture { get; set; }

        public string DataSize { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Picture, StoryPicViewModel>()
               .ForMember(x => x.Path, opt => opt.MapFrom(x => x.Path != null ?
                                                               x.Path :
                                                               x.RemoteImageUrl))
                .ForMember(x => x.DataSize, opt => opt.MapFrom(x => x.Width.ToString() + 'x' + x.Height.ToString()));
        }
    }
}
