using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Medication;
using Hospital.WebProject.ViewModels.Nurse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class NursesController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public NursesController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var docs = await Context.Nurses.Include(x => x.Shift).Include(x => x.Specialization).Include(x => x.User).Select(x => new NurseIndexViewModel
            {
                SpecializationId = x.SpecializationId,
                SpecializationName = x.Specialization.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.Shift.Type,
                UserID = x.UserId,
                UserName = x.User.FirstName + x.User.LastName,
                IsAccepted = x.IsAccepted,
                Image = x.Image
            }).ToListAsync();
            return View(docs);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            var nurseRole = await UserManager.GetUsersInRoleAsync("Nurse");
            var users = nurseRole.Select(u => new
            {
                u.Id,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();
            ViewBag.Users = new SelectList(users, "Id", "FullName");
            ViewBag.Users = new SelectList(users, "Id", "FullName");
            return View(new NurseCreateViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(NurseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var nurse = new Nurse()
            {
                SpecializationId = model.SpecializationId,
                ShiftId = model.ShiftId,
                UserId = model.UserID,
                IsAccepted = model.IsAccepted,
                Image = model.Image,
            };
            await Context.Nurses.AddAsync(nurse);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            var nurse = await Context.Nurses.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            var nurseUser = await UserManager.GetUsersInRoleAsync("Nurse");
            ViewBag.Users = new SelectList(
                nurseUser.Select(u => new { u.Id, FullName = u.FirstName + " " + u.LastName }),
                "Id",
                "FullName",
                nurse.UserId
            );
            var model = new NurseIndexViewModel
            {
                SpecializationId = nurse.SpecializationId,
                ShiftId = nurse.ShiftId,
                UserID = nurse.UserId,
                IsAccepted = nurse.IsAccepted,
                Image = nurse.Image,
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(NurseIndexViewModel model)
        {
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var nurse = await Context.Nurses.FindAsync(model.UserID);
            if (nurse == null)
            {
                return NotFound();
            }
            nurse.SpecializationId = model.SpecializationId;
            nurse.ShiftId = model.ShiftId;
            nurse.UserId = model.UserID;
            nurse.IsAccepted = model.IsAccepted;
            nurse.Image = model.Image;
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var nurse = await Context.Nurses.FindAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            Context.Nurses.Remove(nurse);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
