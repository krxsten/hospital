using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Medication;
using Hospital.WebProject.ViewModels.Nurse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class NursesController : Controller
	{
		private readonly INurseService nurseService;
		private readonly HospitalDbContext context;
		private readonly UserManager<User> userManager;

		public NursesController(
			INurseService nurseService,
			HospitalDbContext context,
			UserManager<User> userManager)
		{
			this.nurseService = nurseService;
			this.context = context;
			this.userManager = userManager;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await nurseService.GetAllAsync();

			var model = dtos.Select(x => new NurseIndexViewModel
			{
				ID = x.ID,
				SpecializationId = x.SpecializationId,
				SpecializationName = x.SpecializationName,
				ShiftId = x.ShiftId,
				ShiftName = x.ShiftName,
				UserID = x.UserID,
				UserName = x.UserName,
				IsAccepted = x.IsAccepted,
				Image = x.Image
			}).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadDropdownsAsync();
			return View(new NurseCreateViewModel());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(NurseCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new NurseCreateDTO
			{
				UserID = model.UserID,
				SpecializationId = model.SpecializationId,
				ShiftId = model.ShiftId,
				IsAccepted = model.IsAccepted,
				Image = model.Image
			};

			await nurseService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			await LoadDropdownsAsync();

			var dto = await nurseService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

			var model = new NurseIndexViewModel
			{
				ID = dto.ID,
				UserID = dto.UserID,
				SpecializationId = dto.SpecializationId,
				ShiftId = dto.ShiftId,
				IsAccepted = dto.IsAccepted,
				Image = dto.Image
			};

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(NurseIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new NurseIndexDTO
			{
				ID = model.ID,
				UserID = model.UserID,
				SpecializationId = model.SpecializationId,
				ShiftId = model.ShiftId,
				IsAccepted = model.IsAccepted,
				Image = model.Image
			};

			await nurseService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			await nurseService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		private async Task LoadDropdownsAsync()
		{
			ViewBag.Shift = new SelectList(
				await context.Shifts.ToListAsync(),
				"ID",
				"Type");

			ViewBag.Specialization = new SelectList(
				await context.Specializations.ToListAsync(),
				"ID",
				"SpecializationName");

			var nurseUsers = await userManager.GetUsersInRoleAsync("Nurse");
			var users = nurseUsers.Select(u => new
			{
				u.Id,
				FullName = u.FirstName + " " + u.LastName
			}).ToList();

			ViewBag.Users = new SelectList(users, "Id", "FullName");
		}
	}
}
