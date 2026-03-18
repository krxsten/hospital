using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Checkup;
using Hospital.WebProject.ViewModels.Diagnose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class CheckupController : Controller
	{
		private readonly ICheckupService checkupService;
		private readonly HospitalDbContext context;

		public CheckupController(ICheckupService checkupService, HospitalDbContext context)
		{
			this.checkupService = checkupService;
			this.context = context;
		}

		[Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var checkups = await checkupService.GetAllAsync();

			var model = checkups.Select(x => new CheckupIndexViewModel
			{
				ID = x.ID,
				Date = x.Date,
				DoctorID = x.DoctorID,
				DoctorName = x.DoctorName,
				Time = x.Time,
				PatientID = x.PatientID,
				PatientName = x.PatientName
			}).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadDropdownsAsync();
			return View(new CheckupCreateViewModel());
		}

		[Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
		[HttpPost]
		public async Task<IActionResult> Create(CheckupCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new CheckupCreateDTO
			{
				Date = model.Date,
				Time = model.Time,
                DoctorID = model.DoctorID,
				PatientID = model.PatientID
			};

			await checkupService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			await LoadDropdownsAsync();

			var checkup = await checkupService.GetByIdAsync(id);
			if (checkup == null)
			{
				return NotFound();
			}

			var model = new CheckupIndexViewModel
			{
				ID = checkup.ID,
				Date = checkup.Date,
				Time = checkup.Time,
                DoctorID = checkup.DoctorID,
				DoctorName = checkup.DoctorName,
				PatientID = checkup.PatientID,
				PatientName = checkup.PatientName
			};

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		public async Task<IActionResult> Edit(CheckupIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new CheckupIndexDTO
			{
				ID = model.ID,
				Date = model.Date,
				Time = model.Time,
                DoctorID = model.DoctorID,
				PatientID = model.PatientID,
				DoctorName = model.DoctorName,
				PatientName = model.PatientName
			};

			await checkupService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			await checkupService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> GetBusyTimes(Guid doctorId, DateTime date)
		{
			var busy = await context.Checkups
				.Where(c => c.DoctorID == doctorId && c.Date.Day == date.Date.Day && c.Date.Month == date.Date.Month && c.Date.Year == date.Date.Year)
				.Select(c => c.Date)
				.ToListAsync();

			return Json(busy);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> GetDoctorShift(Guid doctorId)
		{
			var shift = await context.Doctors
				.Include(d => d.Shift)
				.Where(d => d.ID == doctorId)
				.Select(d => new
				{
					d.Shift.StartTime,
					d.Shift.EndTime
				})
				.FirstOrDefaultAsync();

			if (shift == null)
			{
				return NotFound();
			}

			return Json(shift);
		}

		private async Task LoadDropdownsAsync()
		{
			var doctors = await context.Doctors
				.Include(d => d.User)
				.Select(d => new
				{
					d.ID,
					FullName = d.User.FirstName + " " + d.User.LastName
				})
				.ToListAsync();

			ViewBag.Doctors = new SelectList(doctors, "ID", "FullName");

			var patients = await context.Patients
				.Include(p => p.User)
				.Select(p => new
				{
					p.ID,
					FullName = p.User.FirstName + " " + p.User.LastName
				})
				.ToListAsync();

			ViewBag.Patients = new SelectList(patients, "ID", "FullName");
		}
	}
}
