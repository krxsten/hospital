using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shifts = await Context.Shifts.Select(x => new ShiftViewModel
            {
                Type = x.Type,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToListAsync();
            return View(shifts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ShiftViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShiftViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var shift = new Data.Entities.Shift()
            {
                Type = model.Type,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };
            await Context.Shifts.AddAsync(shift);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
