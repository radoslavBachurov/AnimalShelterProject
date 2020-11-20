using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace AnimalShelter.Web.Infrastructure.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private const string DefaultFileTooBigMessage =
        "Sorry but the maximum size of a single image is 15mb";

        private const string DefaultFilesTooManyMessage =
        "Sorry but you can upload maximum of 20 images";

        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public string FileTooBigMessage { get; set; }

        public string FilesTooManyMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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
            }

            return ValidationResult.Success;
        }

    }
}
