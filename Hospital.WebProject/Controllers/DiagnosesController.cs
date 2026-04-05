using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using System.Security.Claims;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class DiagnosesController : Controller
	{
		private readonly IDiagnoseService diagnoseService;
        private readonly IImageService imageService;

        public DiagnosesController(IDiagnoseService diagnoseService,  IImageService imageService)
		{
			this.diagnoseService = diagnoseService;
			this.imageService = imageService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await diagnoseService.GetAllAsync();

			var model = dtos.Select(x => new DiagnoseIndexViewModel
			{
				ID = x.ID,
				Name = x.Name,
				ImageURL = x.ImageURL
			}).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor")]
		[HttpGet]
		public IActionResult Create()
		{
			return View(new DiagnoseCreateViewModel());
		}

		[Authorize(Roles = "Admin,Doctor")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DiagnoseCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var dto = new DiagnoseCreateDTO
				{
					Name = model.Name,
					ImageFile = model.Image
				};

				await diagnoseService.CreateAsync(dto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Something went wrong");
				return View(model);
			}
		}

		[Authorize(Roles = "Admin,Doctor")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var dto = await diagnoseService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

			var model = new DiagnoseIndexViewModel
			{
				ID = dto.ID,
				Name = dto.Name,
				ImageURL = dto.ImageURL
			};

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DiagnoseIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var dto = new DiagnoseIndexDTO
				{
					ID = model.ID,
					Name = model.Name,
                    ImageURL = model.ImageURL,
					NewImageFile = model.NewImageFile
				};

				await diagnoseService.UpdateAsync(dto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Something went wrong");
				return View(model);
			}
		}

		[Authorize(Roles = "Admin,Doctor")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await diagnoseService.DeleteAsync(id);
			}
			catch (Exception)
			{
				TempData["ErrorMessage"] = "Unable to delete diagnose.";
			}

			return RedirectToAction(nameof(Index));
		}
		[AllowAnonymous]
        public async Task<IActionResult> GetDiagnose(string diagnose)
        {
           var result = await diagnoseService.GetDiagnose(diagnose);
            var model = result.Select(x => new DiagnoseIndexViewModel
            {
                ID = x.ID,
                Name = x.Name,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }
    }
}