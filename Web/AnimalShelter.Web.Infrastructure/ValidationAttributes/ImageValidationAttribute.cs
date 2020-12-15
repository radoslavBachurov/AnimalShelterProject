namespace AnimalShelter.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;

    public class ImageValidationAttribute : ValidationAttribute
    {
        private const string DefaultFileTooBigMessage =
        "Sorry but the maximum size of a single image is 15mb";

        private const string DefaultFilesTooManyMessage =
        "Sorry but you can upload maximum of 20 images";

        private const string DefaultFileNotImageMessage =
        "Uploaded files must be images";

        private readonly int maxFileSize;

        public ImageValidationAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public string FileTooBigMessage { get; set; }

        public string FilesTooManyMessage { get; set; }

        public string FileNotImageMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var files = (List<IFormFile>)value;

                if (files.Count > 20)
                {
                    return new ValidationResult(this.FilesTooManyMessage ?? DefaultFilesTooManyMessage);
                }

                foreach (var file in files)
                {
                    if (file.Length > this.maxFileSize)
                    {
                        return new ValidationResult(this.FileTooBigMessage ?? DefaultFileTooBigMessage);
                    }

                    try
                    {
                        using (var stream = file.OpenReadStream())

                        using (var pic = Image.Load(stream, out IImageFormat format)) ;
                    }
                    catch (Exception)
                    {
                        return new ValidationResult(this.FileNotImageMessage ?? DefaultFileNotImageMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
