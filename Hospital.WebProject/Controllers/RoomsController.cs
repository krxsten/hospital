using Hospital.Data;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly HospitalDbContext Context;
        public RoomsController(HospitalDbContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await Context.Rooms.Select(x => new RoomIndexViewModel
            {
                ID = x.ID,
                RoomNumber = x.RoomNumber,
                IsTaken = x.IsTaken
            }).ToListAsync();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RoomCreateDetailsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomCreateDetailsViewModel model)
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
           // room.PatientsCount++;
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var room = await Context.Rooms.Where(x => x.ID == id).Select(x=>new RoomCreateDetailsViewModel
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
    }
}
