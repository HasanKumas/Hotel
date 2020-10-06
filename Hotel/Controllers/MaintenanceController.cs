using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Models;
using Hotel.Services.Interfaces;
using System;
using System.Linq;

namespace Hotel.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly IRoomService _roomService;

        public MaintenanceController(IRoomService roomService, IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
            _roomService = roomService;
        }

        // GET: Maintenance
        public async Task<IActionResult> Index()
        {
            ViewBag.Succes = 0;
            return View(await _maintenanceService.AllMaintenances());
        }

        // GET: Maintenance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _maintenanceService.GetMaintenance((int)id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }
        public async Task<IActionResult> Block(MaintenanceViewModel maintenance /*string roomNumber, DateTime startDate, DateTime endDate*/)
        {
            var rooms = _roomService.CanBlockedRoomsAsync(maintenance.Room.RoomNumber, maintenance.StartDate, maintenance.EndDate).Result.ToList();
            if (rooms.Count == 1 && rooms[0].RoomNumber.Equals(maintenance.Room.RoomNumber))
            {
                maintenance.Room = rooms[0];
                //add a maintenance in db
                var maintenanceId = _maintenanceService.AddMaintenance(maintenance);

                ViewBag.Message = $"The room number {maintenance.Room.RoomNumber} has blocked succesfully!";
                ViewBag.Controller = "Room";
                ViewBag.Action = "Index";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _maintenanceService.AllMaintenances());
            }
            else if (rooms.Count >= 1)
            {
                ViewBag.Message = $"The room number {maintenance.Room.RoomNumber} is not avaliable for blocking! Please have a look at below alternate rooms to block!";
                ViewBag.StartDate = maintenance.StartDate;
                ViewBag.EndDate = maintenance.EndDate;
                return View(nameof(BlockConfirmation), rooms);
            }
            ViewBag.Message = $"The room number {maintenance.Room.RoomNumber} is not avaliable for blocking! And there is no alternative room to block. Please choose another period.";
            return View(nameof(BlockConfirmation), rooms);
        }

        public IActionResult BlockRoom()
        {
            return View();
        }
        private object BlockConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> BlockAlternate(int? id, DateTime StartDate, DateTime EndDate)
        {
            var room = await _roomService.GetRoom((int)id);
            var maintenance = new MaintenanceViewModel
            {
                StartDate = StartDate,
                EndDate = EndDate,
                Room = room
            };

            //add a maintenance in db
            var maintenanceId = _maintenanceService.AddMaintenance(maintenance);

            ViewBag.Message = $"The room number {room.RoomNumber} has blocked succesfully!";
            ViewBag.Succes = 1;
            return View(nameof(Index), await _maintenanceService.AllMaintenances());
        }


        // GET: Maintenance/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Maintenance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _maintenanceService.GetMaintenance((int)id);
            if (maintenance == null)
            {
                return NotFound();
            }
            return View(maintenance);
        }

        //// POST: Maintenance/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MaintenanceViewModel maintenance)
        {
            if (id != maintenance.MaintenanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool res = _maintenanceService.EditMaintenance(maintenance);
                if (!res)
                {
                    return NotFound();
                }
                ViewBag.Message = $"The maintenance with number {maintenance.MaintenanceId} has updated succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _maintenanceService.AllMaintenances());
            }
            return View(maintenance);
        }

        //// GET: Maintenance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _maintenanceService.GetMaintenance((int)id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        //// POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _maintenanceService.DeleteMaintenance(id);
            ViewBag.Message = $"The maintenance with number {id} has deleted succesfully!";
            ViewBag.Succes = 1;
            return View(nameof(Index), await _maintenanceService.AllMaintenances());
        }
    }
}
