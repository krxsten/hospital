using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Patient;
using Hospital.WebProject.ViewModels.Room;
using Hospital.WebProject.ViewModels.Shift;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class ShiftsController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public ShiftsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shifts = await Context.Shifts.Select(x => new ShiftViewModel
            {
                Type = x.Type,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                ID = x.ID,  
                ListOfDoctors = x.ListOfDoctors,
                ListOfNurses = x.ListOfNurses
                
            }).ToListAsync();
            return View(shifts);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ShiftViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ShiftViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var shift = new Data.Entities.Shift()
            {
                ID = Guid.NewGuid(),
                Type = model.Type,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ListOfDoctors= model.ListOfDoctors,
                ListOfNurses= model.ListOfNurses
            };
            await Context.Shifts.AddAsync(shift);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var shift = await Context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            var model = new ShiftViewModel
            {
                ID = shift.ID,
                Type = shift.Type,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ListOfDoctors = shift.ListOfDoctors,
                ListOfNurses = shift.ListOfNurses
            };
            return View(shift);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ShiftViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var shift = await Context.Shifts.FindAsync(model.ID);
            if (shift == null)
            {
                return NotFound();
            }
            Context.Shifts.Update(shift);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var shift = await Context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            Context.Shifts.Remove(shift);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
