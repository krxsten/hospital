using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.User;
using Hospital.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Hospital.WebProject.Controllers
{

    public class UserController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IImageService imageService;
        private readonly ICityService cityService;

        public UserController(HospitalDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager, IImageService imageService, ICityService cityService)
        {
            this.Context = context;
            this.userManager = userManager;
            this.signManager = signInManager;
            this.roleManager = roleManager;
            this.imageService = imageService;
            this.cityService = cityService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();
            PopulateRegisterDropdowns();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Role == "Patient")
            {
                ModelState.Remove("SpecializationId");
                ModelState.Remove("ShiftId");
                ModelState.Remove("ImageURL");
            }
            else if (model.Role == "Doctor" || model.Role == "Nurse")
            {
                ModelState.Remove("DoctorId");
                ModelState.Remove("RoomId");
                ModelState.Remove("UCN");
                ModelState.Remove("BirthCity");
                ModelState.Remove("DateOfBirth");
            }

            if (!ModelState.IsValid)
            {
                PopulateRegisterDropdowns();
                return View(model);
            }

            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "User with that email address already exists!");
                PopulateRegisterDropdowns();
                return View(model);
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.FirstName+ "_" + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, model.Role);

                string imageUrl = model.Image;
                string publicId = "temp";

                if ((model.Role == "Doctor" || model.Role == "Nurse") && model.ImageURL != null)
                {
                    try
                    {
                        var uploadResult = await imageService.UploadImageAsync(model.ImageURL);
                        imageUrl = uploadResult.Url;
                        publicId = uploadResult.PublicId;
                    }
                    catch (Exception ex)
                    {
                        await userManager.DeleteAsync(user);
                        ModelState.AddModelError("", $"Image upload failed: {ex.Message}");
                        PopulateRegisterDropdowns();
                        return View(model);
                    }
                }

                if (model.Role == "Patient")
                {
                    var patient = new Patient
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        DoctorId = model.DoctorId.Value,
                        RoomId = model.RoomId.Value,
                        PhoneNumber = model.PhoneNumber,
                        BirthCity = model.BirthCity,
                        DateOfBirth = model.DateOfBirth,
                        UCN = model.UCN,
                        HospitalizationDate = DateOnly.FromDateTime(DateTime.Now),
                        HospitalizationTime = TimeOnly.FromDateTime(DateTime.Now),
                        DischargeDate = DateOnly.FromDateTime(DateTime.Now).AddDays(7),
                        DischargeTime = TimeOnly.FromDateTime(DateTime.Now).AddHours(3),
                    };

                    var room = await Context.Rooms.FindAsync(model.RoomId);
                    if (room != null) room.IsTaken = true;

                    Context.Patients.Add(patient);
                }
                else if (model.Role == "Doctor")
                {
                    Context.Doctors.Add(new Doctor
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        SpecializationId = model.SpecializationId.Value,
                        ShiftId = model.ShiftId.Value,
                        IsAccepted = false,
                        ImageURL = imageUrl ?? string.Empty,
                        CloudinaryID = publicId ?? string.Empty,
                    });
                }
                else if (model.Role == "Nurse")
                {
                    Context.Nurses.Add(new Nurse
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        SpecializationId = model.SpecializationId.Value,
                        ShiftId = model.ShiftId.Value,
                        IsAccepted = false,
                        ImageURL = imageUrl ?? string.Empty,
                        PublicID = publicId ?? string.Empty,
                    });
                }

                await Context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            PopulateRegisterDropdowns();
            return View(model);
        }
        private async Task PopulateRegisterDropdowns(string? selectedCity = null)
        {
            ViewBag.Specializations = Context.Specializations.ToList();
            ViewBag.Shifts = Context.Shifts.ToList();
            var docs = Context.Doctors.Include(d => d.User).Select(d => new { d.ID, FullName = d.User.FirstName + " " + d.User.LastName }).ToList();
            ViewBag.Doctors = new SelectList(docs, "ID", "FullName");
            var rooms = Context.Rooms.Where(r => !r.IsTaken).Select(r => new { r.ID, r.RoomNumber }).ToList();
            ViewBag.Rooms = new SelectList(rooms, "ID", "RoomNumber");
            var cities = await cityService.GetAllAsync();
            ViewBag.Cities = new SelectList(cities, selectedCity);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await this.signManager.PasswordSignInAsync(user, model.Password, true, false);

                if (result.Succeeded)
                {
                    var doctor = Context.Doctors.FirstOrDefault(d => d.UserId == user.Id);
                    if (doctor != null && !doctor.IsAccepted)
                    {
                        await signManager.SignOutAsync();
                        ModelState.AddModelError("", "Doctor not approved yet.");
                        return View(model);
                    }

                    var nurse = Context.Nurses.FirstOrDefault(n => n.UserId == user.Id);
                    if (nurse != null && !nurse.IsAccepted)
                    {
                        await signManager.SignOutAsync();
                        ModelState.AddModelError("", "Nurse not approved yet.");
                        return View(model);
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public async Task<IActionResult> SeedRoles()
        {
            string[] roles = { "Admin", "Doctor", "Nurse", "Patient" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
            return Content("Roles seeded (created if missing).");
        }
    }
}
