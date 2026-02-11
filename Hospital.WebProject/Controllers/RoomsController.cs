using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Patient;
using Hospital.WebProject.ViewModels.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public RoomsController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [Authorize(Roles ="Admin, Patient, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await Context.Rooms.Select(x => new RoomViewModel
            {
                ID = x.ID,
                RoomNumber = x.RoomNumber,
                IsTaken = x.IsTaken
            }).ToListAsync();
            return View(rooms);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new RoomViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var room = new Room()
            {
                ID = Guid.NewGuid(),
                RoomNumber = model.RoomNumber,
                IsTaken = model.IsTaken
            };
            await Context.Rooms.AddAsync(room);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Patient, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await Context.Rooms.Where(x => x.ID == id).Select(x=>new RoomViewModel
            {
                IsTaken = x.IsTaken,
                RoomNumber=x.RoomNumber
            }).FirstOrDefaultAsync();

            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await Context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            var model = new RoomViewModel
            {
                ID = room.ID,
                RoomNumber = room.RoomNumber,
                IsTaken = room.IsTaken
            };
            return View(room);
        }
        [Authorize(Roles = "Admin, Doctor, Nurse")]
        [HttpPost]
        public async Task<IActionResult> Edit(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var pat = await Context.Patients.FindAsync(model.ID);
            if (pat == null)
            {
                return NotFound();
            }
            Context.Patients.Update(pat);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pat = await Context.Patients.FindAsync(id);
            if (pat == null)
            {
                return NotFound();
            }
            Context.Patients.Remove(pat);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
