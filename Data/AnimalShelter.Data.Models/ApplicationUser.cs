// ReSharper disable VirtualMemberCallInConstructor
namespace AnimalShelter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

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
            this.PetPosts = new HashSet<PetPost>();
            this.SuccessStories = new HashSet<SuccessStory>();
            this.UserPictures = new HashSet<Picture>();
            this.PostPictures = new HashSet<Picture>();
            this.LikedPosts = new HashSet<UserPetPostLikes>();
            this.LikedSuccessStoryPosts = new HashSet<UserSuccessStoryLikes>();
            this.Replies = new HashSet<Reply>();
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

        public string Description { get; set; }

        public UserSex Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AnswerCounter { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<PetPost> PetPosts { get; set; }

        public virtual ICollection<SuccessStory> SuccessStories { get; set; }

        [InverseProperty("UserPicture")]
        public virtual ICollection<Picture> UserPictures { get; set; }

        [InverseProperty("PostPicture")]
        public virtual ICollection<Picture> PostPictures { get; set; }

        public virtual ICollection<UserPetPostLikes> LikedPosts { get; set; }

        public virtual ICollection<UserSuccessStoryLikes> LikedSuccessStoryPosts { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
