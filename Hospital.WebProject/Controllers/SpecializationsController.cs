using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class SpecializationsController : Controller
	{
		private readonly ISpecializationService specializationService;
        private readonly IImageService imageService;

        public SpecializationsController(ISpecializationService specializationService, IImageService imageService)
		{
			this.specializationService = specializationService;
			this.imageService = imageService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await specializationService.GetAllAsync();

			var model = dtos.Select(x => new SpecializationIndexViewModel
			{
				ID = x.ID,
				SpecializationName = x.SpecializationName,
				ImageURL = x.ImageURL
			}).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Create()
		{
			return View(new SpecializationCreateViewModel());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SpecializationCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var dto = new SpecializationCreateDTO
				{
					SpecializationName = model.SpecializationName,
					ImageFile = model.Image
				};

				await specializationService.CreateAsync(dto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Something went wrong.");
				return View(model);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var dto = await specializationService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

			var model = new SpecializationIndexViewModel
			{
				ID = dto.ID,
				SpecializationName = dto.SpecializationName,
				ImageURL = dto.ImageURL
			};

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(SpecializationIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var dto = new SpecializationIndexDTO
				{
					ID = model.ID,
					SpecializationName = model.SpecializationName,
                    NewImageFile = model.NewImageFile
                };

				await specializationService.UpdateAsync(dto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Something went wrong.");
				return View(model);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await specializationService.DeleteAsync(id);
			}
			catch (Exception)
			{
				TempData["ErrorMessage"] = "Unable to delete specialization.";
			}

			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		[AllowAnonymous]
        public async Task<IActionResult> GetSpecialization(string specialization)
        {
           var result = await specializationService.GetSpecialization(specialization);
            var model = result.Select(x => new SpecializationIndexViewModel
            {
                ID = x.ID,
                SpecializationName = x.SpecializationName,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }
    }
}
