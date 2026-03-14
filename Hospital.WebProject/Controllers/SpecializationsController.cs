using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class SpecializationsController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public SpecializationsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var specializations = await Context.Specializations.Select(x => new SpecializationIndexViewModel
            {
                ID = x.ID,
                SpecializationName = x.SpecializationName,
                Image = x.Image,
            }).ToListAsync();
            return View(specializations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SpecializationCreateViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var spec = new Hospital.Entities.Specialization()
            {
                SpecializationName = model.SpecializationName,
                ID = Guid.NewGuid(),
                Image = model.Image

            };
            await Context.Specializations.AddAsync(spec);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var spec = await Context.Specializations.FindAsync(id);
            if (spec == null)
            {
                return NotFound();
            }
            var model = new SpecializationIndexViewModel
            {
                ID = spec.ID,
                SpecializationName = spec.SpecializationName,
                Image = spec.Image,
            };
            return View(model);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(SpecializationIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var spec = await Context.Specializations.FindAsync(model.ID);
            if (spec == null)
            {
                return NotFound();
            }
            spec.SpecializationName = model.SpecializationName;
            spec.ID = model.ID;
            spec.Image = model.Image;
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spec = await Context.Specializations.FindAsync(id);
            if (spec == null)
            {
                return NotFound();
            }
            Context.Specializations.Remove(spec);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
