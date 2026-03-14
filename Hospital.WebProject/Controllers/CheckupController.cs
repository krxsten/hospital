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
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public CheckupController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var checkup = await Context.Checkups.Select(x => new CheckupIndexViewModel
            {
                ID = x.ID,
                Date = x.Date,
                DoctorName = x.Doctor.User.FirstName + " " + x.Doctor.User.LastName,
                DoctorID = x.DoctorID,
                PatientID = x.PatientID,
                PatientName = x.Patient.User.FirstName + " " + x.Patient.User.LastName

            }).ToListAsync();
            return View(checkup);

        }
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            var docRole = await UserManager.GetUsersInRoleAsync("Doctor");
            var doctors = docRole.Select(d => new
            {
                d.Id,
                FullName = d.FirstName + " " + d.LastName
            }).ToList();
            ViewBag.Doctor = new SelectList(doctors, "Id", "FullName");

            var patientRole = await UserManager.GetUsersInRoleAsync("Patient");
            var users = patientRole.Select(u => new
            {
                u.Id,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            ViewBag.Users = new SelectList(users, "Id", "FullName");

            return View(new CheckupCreateViewModel());
        }
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        [HttpPost]
        public async Task<IActionResult> Create(CheckupCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var checkup = new Checkup()
            {
                ID = Guid.NewGuid(),
                Date = model.Date,
                DoctorID = model.DoctorID,
                PatientID = model.PatientID,
            };
            await Context.Checkups.AddAsync(checkup);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse, Patient")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var doctors = Context.Doctors.Include(d => d.User).Select(d => new
            {
                d.UserId,
                FullName = d.User.FirstName + " " + d.User.LastName
            }).ToList();

            ViewBag.Doctor = new SelectList(doctors, "UserId", "FullName");

            var patients = Context.Patients.Include(p => p.User).Select(p => new
            {
                p.UserId,
                FullName = p.User.FirstName + " " + p.User.LastName
            }).ToList();

            ViewBag.Patients = new SelectList(patients, "UserId", "FullName");

            var checkup = await Context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }
            var model = new CheckupIndexViewModel
            {
                ID = checkup.ID,
                Date = checkup.Date,
                DoctorID = checkup.DoctorID,
                PatientID = checkup.PatientID
            };
            return View(model);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(CheckupIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var checkup = await Context.Checkups.FindAsync(model.ID);
            if (checkup == null)
            {
                return NotFound();
            }
            checkup.Date = model.Date;
            checkup.ID = model.ID;
            checkup.DoctorID = model.DoctorID;
            checkup.PatientID = model.PatientID;
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var checkup = await Context.Checkups.FindAsync(id);
            if (checkup == null)
            {
                return NotFound();
            }
            Context.Checkups.Remove(checkup);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> GetBusyTimes(Guid doctorId, DateTime date)
        {
            var busy = await Context.Checkups.Where(c => c.DoctorID == doctorId && c.Date.Date == date.Date)
            .Select(c => c.Date).ToListAsync();
            return View(busy);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorShift(Guid doctorId)
        {
            var shift = await Context.Doctors.Include(d => d.Shift).Where(d => d.UserId == doctorId)
                .Select(d => new
                {
                    d.Shift.StartTime,
                    d.Shift.EndTime
                }).FirstOrDefaultAsync();
            return View(shift);
        }

    }
}
