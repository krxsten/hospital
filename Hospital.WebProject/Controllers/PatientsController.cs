using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Patient;
using Hospital.WebProject.ViewModels.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;
        private readonly HospitalDbContext context;
        private readonly UserManager<User> userManager;
        private readonly ICityService cityService;
        public PatientsController(
            IPatientService patientService,
            HospitalDbContext context,
            UserManager<User> userManager,
            ICityService cityService)
        {
            this.patientService = patientService;
            this.context = context;
            this.userManager = userManager;
            this.cityService = cityService;
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
        private List<string> LoadBulgarianCities()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/cities.json");
            var jsonData = System.IO.File.ReadAllText(filePath);
            var cities = JsonSerializer.Deserialize<List<string>>(jsonData) ?? new List<string>();

            return cities.OrderBy(c => c).ToList();
        }
        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = new SelectList(LoadBulgarianCities());
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
                await LoadPatientDropdownsAsync();
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.PatientName))
            {
                ModelState.AddModelError("DoctorName", "Doctor name is required.");
                await LoadPatientDropdownsAsync();
                return View(model);
            }
            try
            {
                var dto = new PatientCreateDTO
                {
                    PatientName = model.PatientName,
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadDropdownsAsync();
                TempData["ErrorMessage"] = "Invalid data. Check your input and try again!";
                return View(model);
            }
            
        }

        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Cities = new SelectList(LoadBulgarianCities());
            await LoadPatientDropdownsAsync();

            var dto = await patientService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var model = new PatientEditViewModel
            {
                ID = dto.ID,
                DoctorId = dto.DoctorId,
                HospitalizationDate = dto.HospitalizationDate,
                HospitalizationTime = dto.HospitalizationTime,
                DischargeDate = dto.DischargeDate,
                DischargeTime = dto.DischargeTime,
                RoomId = dto.RoomId,
                BirthCity = dto.BirthCity,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber,
                UCN = dto.UCN,
                PatientName = dto.UserName
            };

            return View(model);
        }

        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }
            try
            {

                var dto = new PatientEditDTO
                {
                    ID = model.ID,
                    DoctorId = model.DoctorId,
                    PatientName = model.PatientName,
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

                await patientService.UpdateAsync(dto);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await patientService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadPatientDropdownsAsync();
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin,Doctor,Nurse")]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
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
                HospitalizationTime = dto.HospitalizationTime,
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
        [Authorize(Roles = "Admin,Doctor,Nurse")]
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

       

        private async Task LoadPatientDropdownsAsync(string? selectedCity = null)
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
            var cities = await cityService.GetAllAsync();

            ViewBag.Cities = cities.Select(c => new SelectListItem
            {
                Value = c,
                Text = c,
                Selected = c == selectedCity,
            }).ToList();
        }
    }
}
