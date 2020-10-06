using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using Hotel.Models;
using System;
using System.Linq;
using Hotel.Data.Models;

namespace Hotel
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMaintenanceService _maintenanceService;

        public RoomController(IRoomService roomService, IMaintenanceService maintenanceService)
        {
            _roomService = roomService;
            _maintenanceService = maintenanceService;
        }
        
        // GET: Room
        public async Task<IActionResult> Index()
        {
            ViewBag.Succes = 0;
            return View(await _roomService.AllRooms());
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoom((int)id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomNumber,RoomType,RoomSize,Price,EntranceAvailableDate")] RoomDetailViewModel room)
        {
            if (ModelState.IsValid)
            {
                _roomService.AddRoom(room);
                ViewBag.Message = $"The room number {room.RoomNumber} has created succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _roomService.AllRooms());
            }
            return View(room);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoom((int)id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        //// POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomNumber,RoomType,RoomSize,Price,EntranceAvailableDate")] RoomDetailViewModel room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               bool res = _roomService.EditRoom(room);
               if(!res)
                {
                    return NotFound();
                }
                ViewBag.Message = $"The room number {room.RoomNumber} has updated succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _roomService.AllRooms());
            }
            return View(room);
        }

        //// GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoom((int)id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        //// POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _roomService.DeleteRoom(id);
            ViewBag.Message = $"The room with id:{id} has deleted succesfully!";
            ViewBag.Succes = 1;
            return View(nameof(Index), await _roomService.AllRooms());
        }
    }
}
