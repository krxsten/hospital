using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Medication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class MedicationsController : Controller
    {
        private readonly IMedicationService medicationService;
        private readonly HospitalDbContext context;

        public MedicationsController(IMedicationService medicationService, HospitalDbContext context)
        {
            this.medicationService = medicationService;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await medicationService.GetAllAsync();

            var model = dtos.Select(x => new MedicationIndexViewModel
            {
                ID = x.ID,
                Name = x.Name,
                DiagnoseID = x.DiagnoseID,
                DiagnoseName = x.DiagnoseName,
                Description = x.Description,
                SideEffects = x.SideEffects
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDiagnosesAsync();
            return View(new MedicationCreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDiagnosesAsync();
                return View(model);
            }

            var dto = new MedicationCreateDTO
            {
                Name = model.Name,
                DiagnoseID = model.DiagnoseID,
                Description = model.Description,
                SideEffects = model.SideEffects
            };

            await medicationService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            await LoadDiagnosesAsync();

            var dto = await medicationService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var model = new MedicationEditViewModel
            {
                ID = dto.ID,
                Name = dto.Name,
                DiagnoseID = dto.DiagnoseID,
                Description = dto.Description,
                SideEffects = dto.SideEffects

            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDiagnosesAsync();
                return View(model);
            }

            var dto = new MedicationEditDTO
            {
                ID = model.ID,
                Name = model.Name,
                DiagnoseID = model.DiagnoseID,
                Description = model.Description,
                SideEffects = model.SideEffects

            };

            await medicationService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await medicationService.DeleteAsync(id);

            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unable to delete medication.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDiagnosesAsync()
        {
            ViewBag.Diagnose = new SelectList(
                await context.Diagnoses.ToListAsync(),
                "ID",
                "Name");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetMedicationsForSideEffect(string sideEffect)
        {
            var dtos = await medicationService.GetMedicationsForSideEffect(sideEffect);
            var model = dtos.Select(x => new MedicationIndexViewModel
            {
                ID = x.ID,
                Name = x.Name,
                DiagnoseID = x.DiagnoseID,
                DiagnoseName = x.DiagnoseName,
                Description = x.Description,
                SideEffects = x.SideEffects

            }).ToList();

            return View(model);
        }
    }
}
