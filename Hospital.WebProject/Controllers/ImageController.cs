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
        //[HttpPost]
        //public async Task<IActionResult> UploadImage(ImageViewModel model)
        //{
        //    try
        //    {
        //        var (url, publicId) = await _imageService.UploadImageAsync(imageFile, name, folder);
        //        return Json(new { url, publicId });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
