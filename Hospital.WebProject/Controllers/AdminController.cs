using Hospital.Core.Contracts;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly HospitalDbContext context;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager;
    private readonly IAdminService adminService;

    public AdminController(HospitalDbContext context, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IAdminService adminService)
    {
        this.context = context;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.userManager = userManager;
        this.adminService = adminService;
    }

    public async Task<IActionResult> PendingDoctors()
    {

        var doctors = await adminService.GetPendingDoctorsAsync();
        ViewBag.Doctors = doctors;
        return View(doctors);

    }

    public async Task<IActionResult> PendingNurses()
    {
        var nurses = await adminService.GetPendingNursesAsync();
        ViewBag.Nurses = nurses;
        return View(nurses);
    }
  
    [HttpPost]
    public async Task<IActionResult> AcceptDoctor(Guid id)
    {
        var result = await adminService.AcceptDoctorAsync(id);

        if (result == null) return NotFound();

        return RedirectToAction("PendingDoctors");
    }
    [HttpPost]
    public async Task<IActionResult> AcceptNurse(Guid id)
    {
        var result = await adminService.AcceptNurseAsync(id);

        if (result == null) return NotFound();

        return RedirectToAction("PendingNurses");
    }
    [HttpPost]
    public async Task<IActionResult> RejectDoctor(Guid id)
    {
        var result = await adminService.RejectDoctorAsync(id);

        if (result == null) return NotFound();

        return RedirectToAction("PendingDoctors");
    }

    [HttpPost]
    public async Task<IActionResult> RejectNurse(Guid id)
    {
        var result = await adminService.RejectNurseAsync(id);

        if (result == null) return NotFound();

        return RedirectToAction("PendingNurses");
    }
}
