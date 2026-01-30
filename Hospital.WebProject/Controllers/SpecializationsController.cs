using Hospital.Data;
using Hospital.WebProject.ViewModels.Shift;
using Hospital.WebProject.ViewModels.Specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class SpecializationsController : Controller
    {
        private readonly HospitalDbContext Context;
        public SpecializationsController(HospitalDbContext context)
        {
            this.Context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var specializations = await Context.Specializations.Select(x => new SpecializationIndexViewModel
            {
                SpecializationName = x.SpecializationName
            }).ToListAsync();
            return View(specializations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SpecializationCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationCreateViewModel model)
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
