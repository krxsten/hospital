using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Medication;
using Hospital.WebProject.ViewModels.Nurse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class NursesController : Controller
    {
        private readonly INurseService nurseService;
        private readonly HospitalDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;

        public NursesController(
            INurseService nurseService,
            HospitalDbContext context,
            UserManager<User> userManager, IImageService imageService)
        {
            this.nurseService = nurseService;
            this.context = context;
            this.userManager = userManager;
            this.imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await nurseService.GetAllAsync();

            var model = dtos.Select(x => new NurseIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                UserID = x.UserId,
                UserName = x.UserName,
                IsAccepted = x.IsAccepted,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new NurseCreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NurseCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.NurseName))
            {
                ModelState.AddModelError("DoctorName", "Doctor name is required.");
                await LoadDropdownsAsync();
                return View(model);
            }

            if (model.Image == null)
            {
                ModelState.AddModelError("Image", "Image is required.");
                await LoadDropdownsAsync();
                return View(model);
            }

            try
            {
                var dto = new NurseCreateDTO
                {
                    NurseName = model.NurseName,
                    SpecializationID = model.SpecializationID,
                    ShiftID = model.ShiftID,
                    IsAccepted = true,
                    ImageFile = model.Image
                };

                await nurseService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            await LoadDropdownsAsync();

            var dto = await nurseService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var model = new NurseEditViewModel
            {
                ID = dto.ID,
                SpecializationId = dto.SpecializationId,
                ShiftId = dto.ShiftId,
                IsAccepted = true,
                ExistingImage = dto.ImageURL,
                NurseName = dto.UserName
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NurseEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }
            try
            {
                var dto = new NurseEditDTO
                {
                    ID = model.ID,
                    SpecializationId = model.SpecializationId,
                    ShiftId = model.ShiftId,
                    IsAccepted = model.IsAccepted,
                    NewImageFile = model.File,
                    NurseName = model.NurseName
                };

                await nurseService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(model);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await nurseService.DeleteAsync(id);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unable to delete nurse.";
            }
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> FilterBySpecialization(string specialization)
        {
            var dtos = await nurseService.FilterBySpecialization(specialization);
            var model = dtos.Select(x => new NurseIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                UserID = x.UserId,
                UserName = x.UserName,
                IsAccepted = x.IsAccepted,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> SortByFirstName()
        {
            var result = await nurseService.SortByFirstName();
            var model = result.Select(x => new NurseIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                UserID = x.UserId,
                UserName = x.UserName,
                IsAccepted = x.IsAccepted,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Shift = new SelectList(
                await context.Shifts.ToListAsync(),
                "ID",
                "Type");

            ViewBag.Specialization = new SelectList(
                await context.Specializations.ToListAsync(),
                "ID",
                "SpecializationName");

            var nurseUsers = await userManager.GetUsersInRoleAsync("Nurse");
            var users = nurseUsers.Select(u => new
            {
                u.Id,
                FullName = u.FirstName + " " + u.LastName
            }).ToList();

            ViewBag.Users = new SelectList(users, "Id", "FullName");
        }
    }
}
