using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Contracts
{
    public interface IImageService
    {
        Task<(string Url, string PublicId)> UploadImageAsync(IFormFile imageFile, string name, string folder);

        Task DestroyImageAsync(string publicId);
    }
}
