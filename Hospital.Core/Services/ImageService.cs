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

        public async Task<(string Url, string PublicId)> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("File is empty or null!");
            }

            var allowedTypes = new[] { "image/jpg", "image/jpeg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
            {
                throw new ArgumentException("Invalid file type. Only JPG, PNG, and WEBP are allowed.");
            }

            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream), 
                DisplayName = imageFile.FileName,
                AssetFolder = "hospital_uploads"
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Cloudinary Upload Failed: {uploadResult.Error.Message}");
            }

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
        private string GetPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;

            var fileName = segments.Last();
            return fileName.Split('.').First();
        }
    }
}
