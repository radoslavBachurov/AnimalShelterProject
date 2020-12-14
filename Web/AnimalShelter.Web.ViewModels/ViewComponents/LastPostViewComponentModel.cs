namespace AnimalShelter.Web.ViewModels.ViewComponents
{
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class LastPostViewComponentModel : IMapFrom<PetPost>, IHaveCustomMappings
    {
        public string Path { get; set; }

        public string DataSize { get; set; }

        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetPost, LastPostViewComponentModel>()
             .ForMember(x => x.Path, opt => opt.MapFrom(x => x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().Path ??
                                                             x.PostPictures.Where(y => y.IsCoverPicture).FirstOrDefault().RemoteImageUrl))

             .ForMember(x => x.DataSize, opt => opt.MapFrom(x => x.PostPictures
                                                                .Where(y => y.IsCoverPicture).FirstOrDefault().Width.ToString() + 'x' +
                                                                x.PostPictures
                                                                .Where(y => y.IsCoverPicture).FirstOrDefault().Height.ToString()));
        }
    }
}
