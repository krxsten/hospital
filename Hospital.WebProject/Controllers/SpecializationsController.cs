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
            var specializations = await Context.Specializations.Select(x => new SpecializationViewModel
            {
                ID = Guid.NewGuid(),
                SpecializationName = x.SpecializationName,
                Image = x.Image,
                ListOfDoctors = x.ListOfDoctors,
                ListOfNurses = x.ListOfNurses,
            }).ToListAsync();
            return View(specializations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SpecializationViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var spec = new Hospital.Entities.Specialization()
            {
                SpecializationName = model.SpecializationName,
                ID = Guid.NewGuid(),
                ListOfDoctors = model.ListOfDoctors,
                ListOfNurses = model.ListOfNurses,
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
            var model = new SpecializationViewModel
            {
                ID = spec.ID,
                SpecializationName = spec.SpecializationName,
                Image = spec.Image,
                ListOfDoctors = spec.ListOfDoctors,
                ListOfNurses = spec.ListOfNurses
            };
            return View(model);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(SpecializationViewModel model)
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
            spec.ListOfDoctors = model.ListOfDoctors;
            spec.ListOfNurses = model.ListOfNurses;
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
