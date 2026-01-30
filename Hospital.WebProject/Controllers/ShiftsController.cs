using Hospital.Data;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Shift;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class ShiftsController : Controller
    {
        private HospitalDbContext context { get; set; }
        public ShiftsController(HospitalDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shifts = await context.Shifts.Select(x => new ShiftIndexViewModel
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
            return View(new ShiftCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShiftCreateViewModel model)
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
            await context.Shifts.AddAsync(shift);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
