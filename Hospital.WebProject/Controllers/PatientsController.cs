using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    public class PatientsController: Controller
    {
        private HospitalDbContext Context { get; set; }
        private readonly UserManager<User> UserManager;
        public PatientsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                var pat = await Context.Patients.Include(x => x.User).Include(x => x.User).Select(x => new DoctorIndexViewModel
                {
                    //SpecializationId = x.SpecializationId,
                    //Specialization = x.Specialization,
                    //ShiftId = x.ShiftId,
                    //Shift = x.Shift,
                    //UserId = x.UserId,
                    //User = x.User,
                    //IsAccepted = x.IsAccepted
                }).ToListAsync();
                return View(pat);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View(new DoctorCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = new Doctor()
            {
                //SpecializationId = model.SpecializationID,
                //Specialization = model.Specialization,
                //ShiftId = model.ShiftID,
                //Shift = model.Shift,
                //UserId = model.UserID,
                //User = model.User,
                //IsAccepted = model.IsAccepted
            };
            await Context.Doctors.AddAsync(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var model = new DoctorCreateViewModel
            {
                //SpecializationID = doctor.SpecializationId,
                //Specialization = doctor.Specialization,
                //ShiftID = doctor.ShiftId,
                //Shift = doctor.Shift,
                //UserID = doctor.UserId,
                //User = doctor.User,
                //IsAccepted = doctor.IsAccepted
            };
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = await Context.Doctors.FindAsync(model.User.UserID);
            if (doctor == null)
            {
                return NotFound();
            }
            Context.Doctors.Update(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            Context.Doctors.Remove(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
