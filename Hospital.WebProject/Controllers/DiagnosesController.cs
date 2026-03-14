using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using System.Security.Claims;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class DiagnosesController : Controller
    {
        private readonly IDiagnoseService diagnoseService;

        public DiagnosesController(IDiagnoseService diagnoseService)
        {
            this.diagnoseService = diagnoseService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await diagnoseService.GetAllAsync();
            return View(dtos);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DiagnoseCreateDTO());
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiagnoseCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await diagnoseService.CreateAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var diagnose = await diagnoseService.GetByIdAsync(id);
            if (diagnose==null)
            {
                return NotFound();
            }
            return View(diagnose);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiagnoseIndexDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await diagnoseService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await diagnoseService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}