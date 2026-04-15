using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
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
        private readonly IShiftService shiftService;

        public ShiftsController(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await shiftService.GetAllAsync();

            var model = dtos.Select(x => new ShiftIndexViewModel
            {
                ID = x.ID,
                Type = x.Type,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ShiftCreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShiftCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new ShiftCreateDTO
                {
                    Type = model.Type,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime
                };

                await shiftService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = await shiftService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var model = new ShiftIndexViewModel
            {
                ID = dto.ID,
                Type = dto.Type,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShiftIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new ShiftIndexDTO
                {
                    ID = model.ID,
                    Type = model.Type,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime
                };

                await shiftService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await shiftService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetShiftByTime(TimeOnly? time = null)
        {
            var result = await shiftService.GetShiftByTime(time);
            
            var model = result.Select(x=> new ShiftIndexViewModel
            {
                ID = x.ID,
                Type = x.Type,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();

            return View(model);
        }
    }
}
