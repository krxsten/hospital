using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hospital.Core.Contracts;
using Hospital.WebProject.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary cloudinary;

        public ImageService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
       
        }

        public async Task<(string Url, string PublicId)> UploadImageAsync(IFormFile imageFile, string name, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("File is empty!");
            }

            var allowedTypes = new[] { "image/jpg", "image/jpeg", "image/png" };
            if (!allowedTypes.Contains(imageFile.ContentType))
            {
                throw new ArgumentException("Invalid file type");
            }

            var uniqieName = $"{Guid.NewGuid()}_{name}";
            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream),
                Folder = folder
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }

        public async Task DestroyImageAsync(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return;
            }

            var deletionParams = new DeletionParams(publicId);
            var result = await cloudinary.DestroyAsync(deletionParams);
        }
    }
}
