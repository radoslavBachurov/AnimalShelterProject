namespace AnimalShelter.Web.ViewModels.StoriesModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class StoryProfileViewModel : IMapFrom<SuccessStory>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string Description { get; set; }

        public string PersonName { get; set; }

        public string PetName { get; set; }

        public int Likes { get; set; }

        public string CreatorNickname { get; set; }

        public bool IsPostLiked { get; set; }

        public bool IsPostCreator { get; set; }

        public string CreatedOn { get; set; }

        public string CoverPicturePath { get; set; }

        public IEnumerable<StoryPicViewModel> Pictures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SuccessStory, StoryProfileViewModel>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Pictures, opt => opt.MapFrom(x => x.PostPictures))
                .ForMember(x => x.PostId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CreatorNickname, opt => opt.MapFrom(x => x.User.Nickname))
                .ForMember(x => x.CoverPicturePath, opt => opt.MapFrom(x => x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path != null ?
                                                                            x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path :
                                                                           x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl));
        }
    }
}
