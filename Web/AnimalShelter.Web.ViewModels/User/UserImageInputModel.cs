namespace AnimalShelter.Web.ViewModels.User
{
    using System.Collections.Generic;

    using AnimalShelter.Common;
    using AnimalShelter.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

    public class UserImageInputModel
    {
        [ImageValidationAttribute(15 * 1024 * 1024, GlobalConstants.MaxUserPhotosUserCanUpload, FilesTooManyMessage = "Sorry but you can upload maximum of 100 images")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
