using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Checkup;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Doctor;
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
            })
            .OrderBy(x => x.Date)
            .ThenBy(x => x.PatientName)
            .ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PatientAppointments()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var patient = context.Patients.FirstOrDefault(x => x.UserId == userId);
            if (patient == null)
            {
                return NotFound();
            }
            var checkups = await checkupService.GetPatientAppointmentAsync(patient.ID);
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
            else
            {
                if (model.Time < doctor.Shift.StartTime || model.Time >= doctor.Shift.EndTime)
                {
                    ModelState.AddModelError("Time", "Selected time is outside doctor's shift.");
                }

                var isBooked = await context.Checkups.AnyAsync(c =>
                    c.DoctorID == model.DoctorID &&
                    c.Date == model.Date &&
                    c.Time == model.Time);

                if (isBooked)
                {
                    ModelState.AddModelError("Time", "This time slot is already booked.");
                }
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

            return RedirectToAction(nameof(PatientAppointments));
        }

        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            await LoadDropdownsAsync();

            var checkup = await checkupService.GetByIdAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }

            var model = new CheckupEditViewModel
            {
                ID = checkup.ID,
                Date = checkup.Date,
                Time = checkup.Time,
                DoctorID = checkup.DoctorID,
                PatientID = checkup.PatientID,
            };
            if (model.Date < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("Date", "Date cannot be in the past.");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(CheckupEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }
            try
            {
                var dto = new CheckupEditDTO
                {
                    ID = model.ID,
                    Date = model.Date,
                    Time = model.Time,
                    DoctorID = model.DoctorID,
                    PatientID = model.PatientID,
                };

                await checkupService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadDropdownsAsync();
                return View(model);
            }
        }

        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await checkupService.DeleteAsync(id);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unable to delete checkup.";
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
        [HttpGet]
        public async Task<IActionResult> GetBusyTimes(Guid doctorId, DateTime date)
        {
            var busyTimes = await checkupService.GetBusyTimes(doctorId, date);
            return View(busyTimes);
        }

        [Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorShift(Guid doctorId)
        {
            var shiftDto = await checkupService.GetDoctorShift(doctorId);

            if (shiftDto == null)
            {
                return NotFound();
            }

            var viewModel = new DoctorShiftViewModel
            {
                StartTime = shiftDto.StartTime,
                EndTime = shiftDto.EndTime
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<JsonResult> GetAvailableSlots(Guid doctorId, DateOnly date)
        {
            var slots = await checkupService.GetAvailableTimeSlotsAsync(doctorId, date);

            var result = slots.Select(t => t.ToString("HH:mm")).ToList();

            return Json(result);
        }

        [Authorize(Roles = "Doctor,Nurse,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetCheckupsDate(DateOnly date)
        {
            var result = await checkupService.GetCheckupsDate(date);
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

