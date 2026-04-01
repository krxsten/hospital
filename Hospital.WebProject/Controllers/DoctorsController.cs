using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
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

		public DoctorsController(
			IDoctorService doctorService,
			HospitalDbContext context,
			UserManager<User> userManager)
		{
			this.doctorService = doctorService;
			this.context = context;
			this.userManager = userManager;
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
		public async Task<IActionResult> Create(DoctorCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new DoctorCreateDto
			{
				UserID = model.UserID,
				SpecializationID = model.SpecializationID,
				ShiftID = model.ShiftID,
				IsAccepted = model.IsAccepted,
				File = model.File
			};

			await doctorService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
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

			var model = new DoctorCreateViewModel
			{
				ID = dto.ID,
				UserID = dto.UserId,
				SpecializationID = dto.SpecializationId,
				ShiftID = dto.ShiftId,
				IsAccepted = dto.IsAccepted,
				//Image = dto.Image
			};

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DoctorCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new DoctorIndexDto
			{
				ID = model.ID,
				UserId = model.UserID,
				SpecializationId = model.SpecializationID,
				ShiftId = model.ShiftID,
				IsAccepted = model.IsAccepted,
				//Image = model.Image
			};

			await doctorService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
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
    }
}
