namespace AnimalShelter.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;

    public class ImageValidationAttribute : ValidationAttribute
    {
        private const string DefaultFileNotImageMessage =
        "Качените файлове трябва да са снимки";

        private readonly string defaultFilesTooManyMessage;

        private readonly string defaultFileTooBigMessage;

        private readonly int maxFileSize;

        private readonly int maxNumberPhotos;

        public ImageValidationAttribute(int maxFileSize, int maxNumberPhotos)
        {
            this.maxFileSize = maxFileSize;
            this.maxNumberPhotos = maxNumberPhotos;
            this.defaultFilesTooManyMessage = $"Можете да качите максимум {maxNumberPhotos} снимки";
            this.defaultFileTooBigMessage = $"Максималният размер за една снимка е {Math.Round(maxFileSize * 0.000001)}мб";
        }

        public string FileTooBigMessage { get; set; }

        public string FilesTooManyMessage { get; set; }

        public string FileNotImageMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var files = (List<IFormFile>)value;

                if (files.Count > this.maxNumberPhotos)
                {
                    return new ValidationResult(this.FilesTooManyMessage ?? this.defaultFilesTooManyMessage);
                }

                foreach (var file in files)
                {
                    if (file.Length > this.maxFileSize)
                    {
                        return new ValidationResult(this.FileTooBigMessage ?? this.defaultFileTooBigMessage);
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
