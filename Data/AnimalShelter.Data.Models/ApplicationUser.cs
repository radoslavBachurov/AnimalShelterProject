// ReSharper disable VirtualMemberCallInConstructor
namespace AnimalShelter.Data.Models
{
    using System;
    using System.Collections.Generic;

    using AnimalShelter.Data.Common.Models;
    using AnimalShelter.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.PetAdoptionPosts = new HashSet<PetAdoptionPost>();
            this.PetLostAndFoundPosts = new HashSet<PetLostAndFoundPost>();
            this.SuccessStories = new HashSet<SuccessStory>();
            this.HomePets = new HashSet<HomePet>();
            this.UserPictures = new HashSet<Picture>();
            this.LikedLostFoundPosts = new HashSet<UserLostFoundPost>();
            this.LikedAdoptionPosts = new HashSet<UserAdoptionPost>();
            this.LikedSuccessStoryPosts = new HashSet<UserSuccessStoryPost>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }

        public string Living { get; set; }

        public Sex Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<PetAdoptionPost> PetAdoptionPosts { get; set; }

        public virtual ICollection<PetLostAndFoundPost> PetLostAndFoundPosts { get; set; }

        public virtual ICollection<SuccessStory> SuccessStories { get; set; }

        public virtual ICollection<HomePet> HomePets { get; set; }

        public virtual ICollection<Picture> UserPictures { get; set; }

        public virtual ICollection<UserAdoptionPost> LikedAdoptionPosts { get; set; }

        public virtual ICollection<UserLostFoundPost> LikedLostFoundPosts { get; set; }

        public virtual ICollection<UserSuccessStoryPost> LikedSuccessStoryPosts { get; set; }
    }
}
