using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public DoctorsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var docs = await Context.Doctors.Include(x => x.Specialization).Include(x => x.Shift).Include(x => x.User).Select(x => new DoctorIndexViewModel
            {
                SpecializationId = x.SpecializationId,
                Specialization = x.Specialization,
                ShiftId = x.ShiftId,
                Shift = x.Shift,
                User = x.User,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                Image = x.Image
            }).ToListAsync();
            return View(docs);

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "Name");
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Name");
            return View(new DoctorCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = new Doctor()
            {
                SpecializationId = model.SpecializationID,
                Specialization = model.Specialization,
                ShiftId = model.ShiftID,
                Shift = model.Shift,
                User = model.User,
                IsAccepted = model.IsAccepted,
                UserId = model.UserID,
                Image = model.Image
            };
            await Context.Doctors.AddAsync(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "Name");
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Name");
            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var model = new DoctorCreateViewModel
            {
                SpecializationID = doctor.SpecializationId,
                Specialization = doctor.Specialization,
                ShiftID = doctor.ShiftId,
                Shift = doctor.Shift,
                User = doctor.User,
                IsAccepted = doctor.IsAccepted,
                UserID = doctor.UserId,
                Image = doctor.Image
            };
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = await Context.Doctors.FindAsync(model.UserID);
            if (doctor == null)
            {
                return NotFound();
            }
            Context.Doctors.Update(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            Context.Doctors.Remove(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
