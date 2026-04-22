using Hospital.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebProject.Controllers
{
    public class ImageController:Controller
    {
        private readonly ImageService _imageService;
        public ImageController(ImageService _imageService)
        {
            this._imageService = _imageService; 
        }
        
    }
}
