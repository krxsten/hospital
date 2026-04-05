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
using System.Security.Claims;

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

        [Authorize(Roles = "Admin,Doctor,Nurse")]
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

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new CheckupCreateViewModel());
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public async Task<IActionResult> Create(CheckupCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var patient = context.Patients.FirstOrDefault(x => x.UserId == userId);
            if (patient == null)
            {
                return NotFound();
            }
            var doctor = await context.Doctors
       .Include(d => d.Shift)
       .FirstOrDefaultAsync(d => d.ID == model.DoctorID);
            if (doctor == null)
            {
                ModelState.AddModelError("DoctorID", "Doctor not found.");
            }

            if (model.Time < doctor.Shift.StartTime || model.Time > doctor.Shift.EndTime)
            {
                ModelState.AddModelError("Time", "Selected time is outside doctor's shift.");
            }

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
                PatientID = patient.ID
            };

            await checkupService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Patient")]
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
            if (model.Date < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("Date", "Date cannot be in the past.");
            }
            return View(model);
        }

        [Authorize(Roles = "Patient")]
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
        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(Guid doctorId, DateOnly date)
        {
            var doctor = await context.Doctors
                .Include(d => d.Shift)
                .FirstOrDefaultAsync(d => d.ID == doctorId);

            if (doctor == null)
                return NotFound();

            var start = doctor.Shift.StartTime;
            var end = doctor.Shift.EndTime;

            var slots = new List<string>();

            var current = start;

            while (current < end)
            {
                slots.Add(current.ToString("HH:mm"));
                current = current.Add(TimeSpan.FromMinutes(30));
            }
            var takenSlots = await context.Checkups.Where(c => c.DoctorID == doctorId && c.Date == date).Select(c => c.Time.ToString()).ToListAsync();
            slots = slots.Where(x => !takenSlots.Contains(x)).ToList();
            return Json(slots);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet]
        public async Task<IActionResult> GetCheckupsAfterDate(DateOnly date)
        {
            var result = await checkupService.GetCheckupsAfterDate(date);
            var model = result.Select(x => new CheckupIndexViewModel
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
        private async Task LoadDropdownsAsync()
        {
            var doctors = await context.Doctors
                .Include(d => d.User)
                .Select(d => new
                {
                    d.ID,
                    FullName = d.User.FirstName + " " + d.User.LastName,
                    Specialization = d.Specialization.SpecializationName
                })
                .ToListAsync();

            ViewBag.Doctors = new SelectList(doctors, "ID", "FullName", "Specialization");

            var patients = await context.Patients
                .Include(p => p.User)
                .Select(p => new
                {
                    p.ID,
                    FullName = p.User.FirstName + " " + p.User.LastName,

                })
                .ToListAsync();

            ViewBag.Patients = new SelectList(patients, "ID", "FullName");
        }

    }
}

