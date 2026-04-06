using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
	[Authorize]
	public class PatientsController : Controller
	{
		private readonly IPatientService patientService;
		private readonly HospitalDbContext context;
		private readonly UserManager<User> userManager;

		public PatientsController(
			IPatientService patientService,
			HospitalDbContext context,
			UserManager<User> userManager)
		{
			this.patientService = patientService;
			this.context = context;
			this.userManager = userManager;
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await patientService.GetAllAsync();

			var model = dtos.Select(x => new PatientIndexViewModel
			{
				ID = x.ID,
				DoctorId = x.DoctorId,
				DoctorName = x.DoctorName,
				HospitalizationDate = x.HospitalizationDate,
				HospitalizationTime = x.HospitalizationTime,
                DischargeDate = x.DischargeDate,
				DischargeTime = x.DischargeTime,	
                UserID = x.UserID,
				UserName = x.UserName,
				RoomId = x.RoomId,
				RoomNumber = x.RoomNumber,
				BirthCity = x.BirthCity,
				DateOfBirth = x.DateOfBirth,
				PhoneNumber = x.PhoneNumber,
				UCN = x.UCN
			}).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadDropdownsAsync();
			return View(new PatientCreateViewModel());
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PatientCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new PatientCreateDTO
			{
				UserID = model.UserID,
				DoctorId = model.DoctorId,
				HospitalizationDate = model.HospitalizationDate,
				HospitalizationTime = model.HospitalizationTime,
                DischargeDate = model.DischargeDate,
				DischargeTime = model.DischargeTime,
                RoomId = model.RoomId,
				BirthCity = model.BirthCity,
				DateOfBirth = model.DateOfBirth,
				PhoneNumber = model.PhoneNumber,
				UCN = model.UCN
			};

			await patientService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			await LoadDropdownsAsync();

			var dto = await patientService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

			var model = new PatientIndexViewModel
			{
				ID = dto.ID,
				DoctorId = dto.DoctorId,
				DoctorName = dto.DoctorName,
				HospitalizationDate = dto.HospitalizationDate,
				HospitalizationTime	= dto.HospitalizationTime,
				DischargeDate = dto.DischargeDate,
				DischargeTime = dto.DischargeTime,
                UserID = dto.UserID,
				UserName = dto.UserName,
				RoomId = dto.RoomId,
				RoomNumber = dto.RoomNumber,
				BirthCity = dto.BirthCity,
				DateOfBirth = dto.DateOfBirth,
				PhoneNumber = dto.PhoneNumber,
				UCN = dto.UCN
			};

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PatientIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadDropdownsAsync();
				return View(model);
			}

			var dto = new PatientIndexDTO
			{
				ID = model.ID,
				DoctorId = model.DoctorId,
				DoctorName = model.DoctorName,
				HospitalizationDate = model.HospitalizationDate,
				HospitalizationTime = model.HospitalizationTime,
                DischargeDate = model.DischargeDate,
				DischargeTime = model.DischargeTime,
                UserID = model.UserID,
				UserName = model.UserName,
				RoomId = model.RoomId,
				RoomNumber = model.RoomNumber,
				BirthCity = model.BirthCity,
				DateOfBirth = model.DateOfBirth,
				PhoneNumber = model.PhoneNumber,
				UCN = model.UCN
			};

			await patientService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			await patientService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
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

			ViewBag.Doctor = new SelectList(doctors, "ID", "FullName");

			ViewBag.Room = new SelectList(
				await context.Rooms.ToListAsync(),
				"ID",
				"RoomNumber");

			var patientUsers = await userManager.GetUsersInRoleAsync("Patient");
			var users = patientUsers.Select(u => new
			{
				u.Id,
				FullName = u.FirstName + " " + u.LastName
			}).ToList();

			ViewBag.Users = new SelectList(users, "Id", "FullName");

            var patients = await context.Patients
                .Include(d => d.User)
                .Select(d => new
                {
                    d.ID,
                    FullName = d.User.FirstName + " " + d.User.LastName
                })
                .ToListAsync();

            ViewBag.Patients = new SelectList(patients, "ID", "FullName");
		}
		[Authorize(Roles ="Admin,Doctor,Nurse")]
		public async Task<IActionResult> PatientsWithSuchDoctor(string doctorName)
		{
			var result = await patientService.PatientsWithSuchDoctor(doctorName);
            var model = result.Select(x => new PatientIndexViewModel
            {
                ID = x.ID,
                DoctorId = x.DoctorId,
                DoctorName = x.DoctorName,
                HospitalizationDate = x.HospitalizationDate,
                HospitalizationTime = x.HospitalizationTime,
                DischargeDate = x.DischargeDate,
                DischargeTime = x.DischargeTime,
                UserID = x.UserID,
                UserName = x.UserName,
                RoomId = x.RoomId,
                RoomNumber = x.RoomNumber,
                BirthCity = x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToList();

            return View(model);
        }

            [Authorize(Roles = "Patient")]
		[HttpGet]
		public async Task<IActionResult> SelectDoctorAndRoom()
		{
			var userId = Guid.Parse(userManager.GetUserId(User)!);
			var existingPatient = await context.Patients
				.AnyAsync(p => p.UserId == userId);

			if (existingPatient)
			{
				TempData["Error"] = "You have already selected a doctor and room.";
				return RedirectToAction("Index", "Home");
			}

			await LoadPatientDropdownsAsync();
			return View(new SelectDoctorAndRoomViewModel());
		}

		[Authorize(Roles = "Patient")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SelectDoctorAndRoom(SelectDoctorAndRoomViewModel model)
		{
			if (!ModelState.IsValid)
			{
				await LoadPatientDropdownsAsync();
				return View(model);
			}

			var userId = Guid.Parse(userManager.GetUserId(User)!);

			try
			{
				await patientService.SelectDoctorAndRoomAsync(
					userId,
					model.DoctorId,
					model.RoomId,
					model.BirthCity,
					model.DateOfBirth,
					model.PhoneNumber,
					model.UCN);

				TempData["Success"] = "You have been successfully registered!";
				return RedirectToAction(nameof(Index));
			}
			catch (InvalidOperationException ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				await LoadPatientDropdownsAsync();
				return View(model);
			}
		}

		private async Task LoadPatientDropdownsAsync()
		{
			var doctors = await context.Doctors
				.Include(d => d.User)
				.Select(d => new
				{
					d.ID,
					FullName = d.User.FirstName + " " + d.User.LastName
				})
				.ToListAsync();

			ViewBag.Doctor = new SelectList(doctors, "ID", "FullName");

			var availableRooms = await context.Rooms
				.Where(r => !r.IsTaken)
				.Select(r => new
				{
					r.ID,
					r.RoomNumber
				})
				.ToListAsync();

			ViewBag.Room = new SelectList(availableRooms, "ID", "RoomNumber");

            var cities = await context.Cities
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .ToListAsync();

            ViewBag.Cities = new SelectList(cities, "Id", "Name");
		}
    }
}
