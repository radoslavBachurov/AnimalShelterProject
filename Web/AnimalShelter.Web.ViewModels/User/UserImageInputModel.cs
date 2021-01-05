namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AnimalShelter.Common;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class UserImageInputModel
    {
        [Required(ErrorMessage = "Не са избрани снимки за качване")]
        [ImageValidationAttribute(GlobalConstants.MaximumSizeOfOnePicture, GlobalConstants.MaxUserPhotosUserCanUpload)]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}