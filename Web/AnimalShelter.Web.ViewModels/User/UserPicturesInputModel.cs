namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class UserPicturesInputModel
    {
        [Required]
        [ImageValidationAttribute(15 * 1024 * 1024)]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
