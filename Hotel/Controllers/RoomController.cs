using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using Hotel.Models;

namespace Hotel
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoomId,RoomNumber,RoomType,RoomSize,Price,EntranceAvailableDate")] RoomDetailViewModel room)
        {
            if (ModelState.IsValid)
            {
                _roomService.AddRoom(room);
                return RedirectToAction(nameof(Index));
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
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoomId,RoomNumber,RoomType,RoomSize,Price,EntranceAvailableDate")] RoomDetailViewModel room)
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
                return RedirectToAction(nameof(Index));
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
        public IActionResult DeleteConfirmed(int id)
        {
            _roomService.DeleteRoom(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}
