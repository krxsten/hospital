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

    public AdminController(HospitalDbContext context, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
    {
        this.context = context;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public async Task<IActionResult> PendingDoctors()
    {
        var doctors = await context.Doctors
            .Include(d => d.User)
            .Where(d => !d.IsAccepted)
            .ToListAsync();


        ViewBag.Doctors = doctors;

        return View(doctors);
    }
       
    public async Task<IActionResult> PendingNurses()
    {

        var nurses = await context.Nurses
            .Include(n => n.User)
            .Where(n => !n.IsAccepted)
            .ToListAsync();
        ViewBag.Nurses = nurses;

        return View(nurses);
    }
    //public async Task<IActionResult> PendingPatients()
    //{

    //    var patients = await context.Patients
    //        .Include(n => n.User)
    //        .Where(n => !n.IsAccepted)
    //        .ToListAsync();
    //    ViewBag.Patients = patients;

    //    return View(patients);
    //}
    [HttpPost]
    public async Task<IActionResult> AcceptDoctor(Guid id)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.ID == id);

        if (doctor == null)
            return NotFound();

        if (doctor.IsAccepted)
            return RedirectToAction("PendingDoctors");

        doctor.IsAccepted = true;
        await context.SaveChangesAsync();

        return RedirectToAction("PendingDoctors");
    }

    [HttpPost]
    public async Task<IActionResult> AcceptNurse(Guid id)
    {
        var nurse = await context.Nurses.FirstOrDefaultAsync(n => n.UserId == id);

        if (nurse == null)
            return NotFound();

        if (nurse.IsAccepted)
            return RedirectToAction("PendingNurses");

        nurse.IsAccepted = true;
        await context.SaveChangesAsync();

        return RedirectToAction("PendingUsers");
    }
}
