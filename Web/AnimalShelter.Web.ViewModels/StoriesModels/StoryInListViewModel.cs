namespace AnimalShelter.Web.ViewModels.StoriesModels
{
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class StoryInListViewModel : IMapFrom<SuccessStory>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PetName { get; set; }

        public int Likes { get; set; }

        public string CreatedOn { get; set; }

        public string CoverPicturePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SuccessStory, StoryInListViewModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.PostId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description.Substring(0, 60) + "..."))
                .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                            x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                           x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
