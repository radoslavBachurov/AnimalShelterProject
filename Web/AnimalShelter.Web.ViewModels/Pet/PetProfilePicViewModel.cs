using AnimalShelter.Data.Models;
using AnimalShelter.Services.Mapping;
using AutoMapper;

namespace AnimalShelter.Web.ViewModels.Pet
{
    public class PetProfilePicViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        public string Path { get; set; }

        public bool IsCoverPicture { get; set; }

        public string DataSize { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Picture, PetProfilePicViewModel>()
               .ForMember(x => x.Path, opt => opt.MapFrom(x => x.Path != null ?
                                                               x.Path :
                                                               x.RemoteImageUrl))
                .ForMember(x => x.DataSize, opt => opt.MapFrom(x => x.Width.ToString() + 'x' + x.Height.ToString()));
        }
    }
}
