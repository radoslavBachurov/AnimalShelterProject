namespace AnimalShelter.Web.ViewModels.Administration.ApprovalPost
{
    using System;
    using System.Globalization;

    using AnimalShelter.Data.Models;
    using AnimalShelter.Services.Mapping;
    using AnimalShelter.Web.ViewModels.Pet;
    using AutoMapper;

    public class PetApprovalViewModel : IMapFrom<PetPost>, IHaveCustomMappings
    {
        public int PostId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorNickname { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PetPost, PetApprovalViewModel>()
                .ForMember(x => x.PostId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CreatorNickname, opt => opt.MapFrom(x => x.User.Nickname));
        }
    }
}
