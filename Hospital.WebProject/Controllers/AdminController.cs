using Hospital.Data;
using Hospital.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly HospitalDbContext context;

    public AdminController(HospitalDbContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> PendingUsers()
    {
        var doctors = await context.Doctors
            .Include(d => d.User)
            .Where(d => !d.IsAccepted)
            .ToListAsync();

        var nurses = await context.Nurses
            .Include(n => n.User)
            .Where(n => !n.IsAccepted)
            .ToListAsync();

        ViewBag.Doctors = doctors;
        ViewBag.Nurses = nurses;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AcceptDoctor(Guid id)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == id);
        if (doctor == null)
        {
            return NotFound();
        }
        doctor.IsAccepted = true;
        await context.SaveChangesAsync();
        return RedirectToAction("PendingUsers");
    }

    [HttpPost]
    public async Task<IActionResult> AcceptNurse(Guid id)
    {
        var nurse = await context.Nurses.FirstOrDefaultAsync(n => n.UserId == id);
        if (nurse == null)
        {
            return NotFound();
        }
        nurse.IsAccepted = true;
        await context.SaveChangesAsync();
        return RedirectToAction("PendingUsers");
    }
}
