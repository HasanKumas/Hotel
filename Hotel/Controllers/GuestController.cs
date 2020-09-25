using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using GuestViewModel = Hotel.Models.GuestViewModel;

namespace Hotel
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: Guest
        public async Task<IActionResult> Index()
        {
            return View(await _guestService.AllGuests());
        }

        // GET: Guest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuest((int)id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(GuestViewModel guest)
        {
            if (ModelState.IsValid)
            {
                _guestService.AddGuest(guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: Guest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuest((int)id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        //// POST: Guest/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(int id, GuestViewModel guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool res = _guestService.EditGuest(guest);
                if (!res)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        //// GET: Guest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuest((int)id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        //// POST: Guest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _guestService.DeleteGuest(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
