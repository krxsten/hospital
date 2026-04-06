using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class DoctorsController : Controller
	{
		private readonly IDoctorService doctorService;
		private readonly HospitalDbContext context;
		private readonly UserManager<User> userManager;
		private readonly IImageService imageService;

		public DoctorsController(
			IDoctorService doctorService,
			HospitalDbContext context,
			UserManager<User> userManager, IImageService imageService)
		{
			this.doctorService = doctorService;
			this.context = context;
			this.userManager = userManager;
			this.imageService = imageService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await doctorService.GetAllAsync();

			var model = dtos.Select(x => new DoctorIndexViewModel
			{
				ID = x.ID,
				SpecializationId = x.SpecializationId,
				SpecializationName = x.SpecializationName,
				ShiftId = x.ShiftId,
				ShiftName = x.ShiftName,
				IsAccepted = x.IsAccepted,
				UserId = x.UserId,
				UserName = x.UserName,
				ImageURL = x.ImageURL,
            }).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadDropdownsAsync();
			return View(new DoctorCreateViewModel());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            try
            {
                var dto = new DoctorCreateDto
                {
                    UserID = model.UserID,
                    SpecializationID = model.SpecializationID,
                    ShiftID = model.ShiftID,
                    IsAccepted = model.IsAccepted,
                    ImageFile = model.Image,
                };

                await doctorService.CreateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }
           
        }

        [Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			await LoadDropdownsAsync();

			var dto = await doctorService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

            var model = new DoctorEditViewModel
            {
                ID = dto.ID,
                SpecializationId = dto.SpecializationId,
                ShiftId = dto.ShiftId,
                IsAccepted = dto.IsAccepted,
                ExistingImage = dto.ImageURL
            };

            return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DoctorEditViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

            try
            {
                var dto = new DoctorEditDTO
                {
                    ID = model.ID,
                    SpecializationId = model.SpecializationId,
                    ShiftId = model.ShiftId,
                    IsAccepted = model.IsAccepted,
                    ImageURL = model.ExistingImage,
                    NewImageFile = model.File
                };


                await doctorService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }
           
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			await doctorService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		private async Task LoadDropdownsAsync()
		{
			var users = await userManager.Users
				.Select(u => new
				{
					u.Id,
					FullName = u.FirstName + " " + u.LastName
				})
				.ToListAsync();

			ViewBag.Users = new SelectList(users, "Id", "FullName");

			ViewBag.Specialization = new SelectList(
				await context.Specializations.ToListAsync(),
				"ID",
				"SpecializationName");

			ViewBag.Shift = new SelectList(
				await context.Shifts.ToListAsync(),
				"ID",
				"Type");
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Doctor,Nurse")]
		public async Task<IActionResult> GetDoctorShift(Guid doctorId)
		{
			var shift = await context.Doctors
				.Include(d => d.Shift)
				.Where(d => d.ID == doctorId)
				.Select(d => new DoctorShiftViewModel
				{
					StartTime = d.Shift.StartTime,
					EndTime = d.Shift.EndTime
				})
				.FirstOrDefaultAsync();

			if (shift == null)
			{
				return NotFound();
			}

			return View(shift);
		}
		[AllowAnonymous]
        public async Task<IActionResult> FilterBySpecialization(string specialization)
        {
            var dtos = await doctorService.FilterBySpecialization(specialization);
            var model = dtos.Select(x => new DoctorIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                ImageURL = x.ImageURL,
            }).ToList();
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> SortByFirstName()
        {
           var result = await doctorService.SortByFirstName();
            var model = result.Select(x => new DoctorIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                ImageURL = x.ImageURL,
            }).ToList();
            return View(model);
        }
    }
}
