using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                SpecializationName = x.SpecializationName
            }).ToListAsync();
            return View(specializations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SpecializationViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var spec = new Hospital.Entities.Specialization()
            {
                SpecializationName = model.SpecializationName
            };
            await Context.Specializations.AddAsync(spec);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
