namespace AnimalShelter.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using AnimalShelter.Data.Common.Models;

    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Path { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public bool IsCoverPicture { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? HomePetId { get; set; }

        public HomePet HomePet { get; set; }

        public int? PetAdoptionPostId { get; set; }

        public PetAdoptionPost PetAdoptionPost { get; set; }

        public int? PetLostAndFoundPostId { get; set; }

        public PetLostAndFoundPost PetLostAndFoundPost { get; set; }

        public int? SuccessStoryId { get; set; }

        public SuccessStory SuccessStory { get; set; }
    }
}
