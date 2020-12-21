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

        public int Width { get; set; }

        public int Height { get; set; }

        public string UserPictureId { get; set; }

        public ApplicationUser UserPicture { get; set; }

        public string PostPictureId { get; set; }

        public ApplicationUser PostPicture { get; set; }

        public int? PetPostId { get; set; }

        public PetPost PetPost { get; set; }

        public int? SuccessStoryId { get; set; }

        public SuccessStory SuccessStory { get; set; }
    }
}
