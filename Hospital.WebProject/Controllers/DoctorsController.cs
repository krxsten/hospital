using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Hospital.WebProject.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
                ShiftId = x.ShiftId,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                Image = x.Image,
            }).ToListAsync();
            return View(docs);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userDb = await UserManager.Users.ToListAsync();
            var users = userDb.Select(u => new
            {
                u.Id,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            ViewBag.Users = new SelectList(users, "Id", "FullName");
            ViewBag.Specialization = new SelectList(await Context.Specializations.ToListAsync(), "ID", "SpecializationName");
            ViewBag.Shift = new SelectList(await Context.Shifts.ToListAsync(), "ID", "Type");
            return View(new DoctorCreateViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = new Doctor()
            {
                UserId = model.UserID,
                SpecializationId = model.SpecializationID,
                ShiftId = model.ShiftID,
                IsAccepted = model.IsAccepted,
                Image = model.Image
            };
            await Context.Doctors.AddAsync(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            var doctorUsers = await UserManager.GetUsersInRoleAsync("Doctor");
            ViewBag.Users = new SelectList(
                doctorUsers.Select(u => new { u.Id, FullName = u.FirstName + " " + u.LastName }),
                "Id",
                "FullName",
                doctor.UserId
            );
            var model = new DoctorCreateViewModel
            {
                SpecializationID = doctor.SpecializationId,
                ShiftID = doctor.ShiftId,
                IsAccepted = doctor.IsAccepted,
                UserID = doctor.UserId,
                Image = doctor.Image,
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
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
            doctor.UserId = model.UserID;
            doctor.SpecializationId = model.SpecializationID;
            doctor.ShiftId = model.ShiftID;
            doctor.IsAccepted = model.IsAccepted;
            doctor.Image = model.Image;
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
