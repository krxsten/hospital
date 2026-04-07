using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.WebProject.ViewModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly HospitalDbContext context;

        public DoctorsController(
            IDoctorService doctorService,
            HospitalDbContext context)
        {
            this.doctorService = doctorService;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await doctorService.GetAllAsync();

            var model = dtos.Select(x => new DoctorIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                UserName = x.UserName,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new DoctorCreateViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.DoctorName))
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
                var dto = new DoctorCreateDto
                {
                    DoctorName = model.DoctorName,
                    SpecializationID = model.SpecializationID,
                    ShiftID = model.ShiftID,
                    IsAccepted = model.IsAccepted,
                    ImageFile = model.Image
                };

                await doctorService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadDropdownsAsync();
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            await LoadDropdownsAsync();

            var dto = await doctorService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }

            var model = new DoctorEditViewModel
            {
                ID = dto.ID,
                SpecializationId = dto.SpecializationId,
                ShiftId = dto.ShiftId,
                IsAccepted = dto.IsAccepted,
                ExistingImage = dto.ImageURL,
                DoctorName = dto.UserName
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            try
            {
                var dto = new DoctorEditDTO
                {
                    ID = model.ID,
                    SpecializationId = model.SpecializationId,
                    ShiftId = model.ShiftId,
                    IsAccepted = model.IsAccepted,
                    NewImageFile = model.File,
                    DoctorName = model.DoctorName
                };

                await doctorService.UpdateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadDropdownsAsync();
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await doctorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            ViewBag.Specialization = new SelectList(
                await context.Specializations.ToListAsync(),
                "ID",
                "SpecializationName");

            ViewBag.Shift = new SelectList(
                await context.Shifts.ToListAsync(),
                "ID",
                "Type");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Nurse")]
        public async Task<IActionResult> GetDoctorShift(Guid doctorId)
        {
            var shift = await context.Doctors
                .Include(d => d.Shift)
                .Where(d => d.ID == doctorId)
                .Select(d => new DoctorShiftViewModel
                {
                    StartTime = d.Shift.StartTime,
                    EndTime = d.Shift.EndTime
                })
                .FirstOrDefaultAsync();

            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FilterBySpecialization(string specialization)
        {
            var dtos = await doctorService.FilterBySpecialization(specialization);

            var model = dtos.Select(x => new DoctorIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                UserName = x.UserName,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SortByFirstName()
        {
            var result = await doctorService.SortByFirstName();

            var model = result.Select(x => new DoctorIndexViewModel
            {
                ID = x.ID,
                SpecializationId = x.SpecializationId,
                SpecializationName = x.SpecializationName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftName,
                IsAccepted = x.IsAccepted,
                UserId = x.UserId,
                UserName = x.UserName,
                ImageURL = x.ImageURL
            }).ToList();

            return View(model);
        }
    }
}