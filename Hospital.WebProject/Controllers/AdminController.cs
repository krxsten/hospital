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
        .Include(d => d.Specialization)
        .Include(d => d.Shift)
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
        {
            return NotFound();
        }
        doctor.IsAccepted = true;
        await context.SaveChangesAsync();

        return RedirectToAction("PendingDoctors");
    }
    [HttpPost]
    public async Task<IActionResult> AcceptNurse(Guid id)
    {
        var nurse = await context.Nurses.FirstOrDefaultAsync(n => n.ID == id); 

        if (nurse == null)
        {
            return NotFound();
        }

        nurse.IsAccepted = true;
        await context.SaveChangesAsync();

        return RedirectToAction("PendingNurses"); 
    }
    [HttpPost]
    public async Task<IActionResult> RejectDoctor(Guid id)
    {
        var doctor = await context.Doctors.FindAsync(id);

        if (doctor == null)
            return NotFound();

        var user = await userManager.FindByIdAsync(doctor.UserId.ToString());

        context.Doctors.Remove(doctor);

        if (user != null)
            await userManager.DeleteAsync(user);

        await context.SaveChangesAsync();

        return RedirectToAction("PendingDoctors");
    }

    [HttpPost]
    public async Task<IActionResult> RejectNurse(Guid id)
    {
        var nurse = await context.Nurses.FindAsync(id);

        if (nurse == null)
            return NotFound();

        var user = await userManager.FindByIdAsync(nurse.UserId.ToString());

        context.Nurses.Remove(nurse);

        if (user != null)
            await userManager.DeleteAsync(user);

        await context.SaveChangesAsync();

        return RedirectToAction("PendingNurses");
    }
}
