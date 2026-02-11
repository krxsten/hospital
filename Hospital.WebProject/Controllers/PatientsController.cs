using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private HospitalDbContext Context { get; set; }
        private readonly UserManager<User> UserManager;
        public PatientsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [Authorize(Roles = "Admin, Doctor, Patient, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pat = await Context.Patients.Include(x => x.User).Include(x => x.Doctor).Include(x => x.Room).Select(x => new PatientViewModel
            {
                Doctor = x.Doctor,
                DoctorId = x.DoctorId,
                HospitalizationDate = x.HospitalizationDate,
                DischargeDate = x.DischargeDate,
                UserID = x.UserId,
                User = x.User,
                Room = x.Room,
                RoomId = x.RoomId,
                BirthCity = x.BirthCity,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                UCN = x.UCN
            }).ToListAsync();
            return View(pat);

        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var doctors = Context.Doctors.Include(d => d.User).Select(d => new
            {
                d.UserId,
                FullName = d.User.FirstName + " " + d.User.LastName
            }).ToList();

            ViewBag.Doctor = new SelectList(doctors, "UserId", "FullName");
            ViewBag.Room = new SelectList(Context.Rooms, "ID", "RoomNumber");
            var patientRole = await UserManager.GetUsersInRoleAsync("Patient");
            var users = patientRole.Select(u => new
            {
                u.Id,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            ViewBag.Users = new SelectList(users, "Id", "FullName");
            return View(new PatientViewModel());
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var pat = new Patient()
            {
                Doctor = model.Doctor,
                DoctorId = model.DoctorId,
                HospitalizationDate = model.HospitalizationDate,
                DischargeDate = model.DischargeDate,
                UserId = model.UserID,
                User = model.User,
                Room = model.Room,
                RoomId = model.RoomId,
                BirthCity = model.BirthCity,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                UCN = model.UCN
            };
            await Context.Patients.AddAsync(pat);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var doctors = Context.Doctors.Include(d => d.User).Select(d => new
            {
                d.UserId,
                FullName = d.User.FirstName + " " + d.User.LastName
            }).ToList();
            ViewBag.Doctor = new SelectList(doctors, "UserId", "FullName");
            ViewBag.Room = new SelectList(Context.Rooms, "ID", "RoomNumber");
            var pat = await Context.Patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }
            var patientRole = await UserManager.GetUsersInRoleAsync("Patient");
            ViewBag.Users = new SelectList(
                patientRole.Select(u => new { u.Id, FullName = u.FirstName + " " + u.LastName }),
                "Id",
                "FullName",
                pat.UserId
            );
            var model = new PatientViewModel
            {
                Doctor = pat.Doctor,
                DoctorId = pat.DoctorId,
                HospitalizationDate = pat.HospitalizationDate,
                DischargeDate = pat.DischargeDate,
                UserID = pat.UserId,
                User = pat.User,
                Room = pat.Room,
                RoomId = pat.RoomId,
                BirthCity = pat.BirthCity,
                DateOfBirth = pat.DateOfBirth,
                PhoneNumber = pat.PhoneNumber,
                UCN = pat.UCN
            };
            return View(pat);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(PatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var pat = await Context.Patients.FindAsync(model.UserID);
            if (pat == null)
            {
                return NotFound();
            }
            Context.Patients.Update(pat);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pat = await Context.Patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }
            Context.Patients.Remove(pat);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
