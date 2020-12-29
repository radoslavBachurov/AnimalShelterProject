namespace AnimalShelter.Web.ViewModels.Pet
{
    using System;
    using System.Linq;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AutoMapper;

    public class PetReplyViewModel : IMapFrom<Reply>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int? ParentId { get; set; }

        public string UsernickName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserProfilePicPath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, PetReplyViewModel>()
           .ForMember(x => x.UsernickName, opt => opt.MapFrom(x => x.User.Nickname))
           .ForMember(x => x.UserProfilePicPath, opt => opt.MapFrom(x => x.User.UserPictures.FirstOrDefault(x => x.IsCoverPicture).Path));
        }
    }
}
