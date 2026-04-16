using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
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
		private readonly IRoomService roomService;

		public RoomsController(IRoomService roomService)
		{
			this.roomService = roomService;
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dtos = await roomService.GetAllAsync();

			var model = dtos.Select(x => new RoomIndexViewModel
			{
				ID = x.ID,
				RoomNumber = x.RoomNumber,
				IsTaken = x.IsTaken
			}).OrderBy(x => x.RoomNumber).ToList();

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Create()
		{
			return View(new RoomCreateViewModel());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(RoomCreateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var dto = new RoomCreateDTO
			{
				RoomNumber = model.RoomNumber,
				IsTaken = model.IsTaken
			};

			await roomService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var dto = await roomService.GetByIdAsync(id);
			if (dto == null)
			{
				return NotFound();
			}

			var model = new RoomIndexViewModel
			{
				ID = dto.ID,
				RoomNumber = dto.RoomNumber,
				IsTaken = dto.IsTaken
			};

			return View(model);
		}

		[Authorize(Roles = "Admin,Doctor,Nurse")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RoomIndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var dto = new RoomIndexDTO
			{
				ID = model.ID,
				RoomNumber = model.RoomNumber,
				IsTaken = model.IsTaken
			};

			await roomService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			await roomService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
        [Authorize(Roles = "Admin,Doctor,Nurse")]
        public async Task<IActionResult> GetRoomsAfterNum(int? roomNum)
		{
            if (!roomNum.HasValue)
            {
                return View(new List<RoomIndexViewModel>());
            }

            var result = await roomService.GetRoomsAfterNum(roomNum.Value);
            var model = result.Select(x => new RoomIndexViewModel
            {
                ID = x.ID,
                RoomNumber = x.RoomNumber,
                IsTaken = x.IsTaken
            }).OrderBy(x => x.RoomNumber).ToList();
			return View(model);
        }
    }
}
