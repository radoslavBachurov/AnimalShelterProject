namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;

    using AnimalShelter.Common;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class UserImageInputModel
    {
        [ImageValidationAttribute(GlobalConstants.MaximumSizeOfOnePicture, GlobalConstants.MaxUserPhotosUserCanUpload)]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}