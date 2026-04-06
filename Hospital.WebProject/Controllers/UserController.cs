using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.WebProject.Controllers
{

    public class UserController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserController(HospitalDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.Context = context;
            this.userManager = userManager;
            this.signManager = signInManager;
            this.roleManager = roleManager;
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
            if (!ModelState.IsValid)
            {
                PopulateRegisterDropdowns();
                return View(model);
            }
            if (model.Role == "Doctor" || model.Role == "Nurse")
            {
                if (model.SpecializationId == null || model.ShiftId == null)
                {
                    ModelState.AddModelError("", "Specialization and shift are required.");
                    PopulateRegisterDropdowns();
                    return View(model);
                }
            }
            if (model.Role == "Patient")
            {
                if (model.DoctorId == null || model.RoomId == null)
                {
                    ModelState.AddModelError("", "Doctor and room are required.");
                    PopulateRegisterDropdowns();
                    return View(model);
                }
                var roomChk = Context.Rooms.Find(model.RoomId);
                if (roomChk == null || roomChk.IsTaken)
                {
                    ModelState.AddModelError("", "Selected room is not available.");
                    PopulateRegisterDropdowns();
                    return View(model);
                }
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.FirstName + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, model.Role);

                if (model.Role == "Patient")
                {
                    var birthCityName = model.BirthCity;
                    if (Guid.TryParse(model.BirthCity, out var cityId))
                    {
                        var city = await Context.Cities.FindAsync(cityId);
                        if (city != null) birthCityName = city.Name;
                    }

                    var patient = new Patient
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        DoctorId = model.DoctorId.Value,
                        RoomId = model.RoomId.Value,
                        PhoneNumber = model.PhoneNumber,
                        BirthCity = birthCityName,
                        DateOfBirth = model.DateOfBirth,
                        UCN = model.UCN,
                        HospitalizationDate = DateOnly.FromDateTime(DateTime.Now),
                        HospitalizationTime = TimeOnly.FromDateTime(DateTime.Now),
                    };

                    var selectedRoom = Context.Rooms.Find(model.RoomId);
                    if (selectedRoom != null)
                    {
                        selectedRoom.IsTaken = true;
                    }

                    Context.Patients.Add(patient);
                }
                else if (model.Role == "Doctor")
                {
                    if (model.SpecializationId == null || model.ShiftId == null)
                    {
                        ModelState.AddModelError("", "Doctor must have specialization and shift.");
                        PopulateRegisterDropdowns();
                        return View(model);
                    }
                    var doctor = new Doctor
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        SpecializationId = model.SpecializationId.Value,
                        ShiftId = model.ShiftId.Value,
                        ImageURL = model.Image,
                        CloudinaryID = "temp",
                        IsAccepted = false
                    };

                    Context.Doctors.Add(doctor);
                    TempData["Message"] = "Registration successful. Waiting for admin approval.";
                }
                else if (model.Role == "Nurse")
                {
                    if (model.SpecializationId == null || model.ShiftId == null)
                    {
                        ModelState.AddModelError("", "Nurse must have specialization and shift.");
                        PopulateRegisterDropdowns();
                        return View(model);
                    }
                    var nurse = new Nurse
                    {
                        ID = Guid.NewGuid(),
                        UserId = user.Id,
                        SpecializationId = model.SpecializationId.Value,
                        ShiftId = model.ShiftId.Value,
                        ImageURL = model.Image,
                        PublicID = "temp",
                        IsAccepted = false
                    };

                    Context.Nurses.Add(nurse);
                    TempData["Message"] = "Registration successful. Waiting for admin approval.";
                }

                await Context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            PopulateRegisterDropdowns();
            return View(model);
        }

        private void PopulateRegisterDropdowns()
        {
            ViewBag.Specializations = Context.Specializations.ToList();
            ViewBag.Shifts = Context.Shifts.ToList();
            var docs = Context.Doctors.Include(d => d.User).Select(d => new { d.ID, FullName = d.User.FirstName + " " + d.User.LastName }).ToList();
            ViewBag.Doctors = new SelectList(docs, "ID", "FullName");
            var rooms = Context.Rooms.Where(r => !r.IsTaken).Select(r => new { r.ID, r.RoomNumber }).ToList();
            ViewBag.Rooms = new SelectList(rooms, "ID", "RoomNumber");
            var cities = Context.Cities.OrderBy(c => c.Name).Select(c => new { c.Id, c.Name }).ToList();
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
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
