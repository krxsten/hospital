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
            var checkup = await Context.Checkups.Select(x => new CheckupViewModel
            {
                ID = Guid.NewGuid(),
                Date = new DateTime(),
                Doctor = x.Doctor,
                DoctorID = x.DoctorID,
                PatientID = x.PatientID,
                Patient = x.Patient

            }).ToListAsync();
            return View(checkup);

        }
        [HttpGet]
        public IActionResult Create()
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

            return View(new CheckupViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CheckupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var checkup = new Checkup()
            {
                ID = Guid.NewGuid(),
                Date = new DateTime(),
                Doctor = model.Doctor,
                DoctorID = model.DoctorID,
                PatientID = model.PatientID,
                Patient = model.Patient
            };
            await Context.Checkups.AddAsync(checkup);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
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
            var model = new CheckupViewModel
            {
                ID = Guid.NewGuid(),
                Date = new DateTime(),
                Doctor = checkup.Doctor,
                DoctorID = checkup.DoctorID,
                PatientID = checkup.PatientID,
                Patient = checkup.Patient
            };
            return View(checkup);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CheckupViewModel model)
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
            Context.Checkups.Update(checkup);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
    }
}
