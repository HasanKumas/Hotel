using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using GuestViewModel = Hotel.Models.GuestViewModel;
using System.Collections.Generic;

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
            ViewBag.Succes = 0;
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
        [HttpPost]
        public async Task<IActionResult> Create(GuestViewModel guest)
        {
            if (ModelState.IsValid)
            {
                _guestService.AddGuest(guest);
                ViewBag.Message = $"The guest {guest.LastName} has registered succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _guestService.AllGuests());
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
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GuestViewModel guest)
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
                ViewBag.Message = $"The guest  {guest.LastName} has updated succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _guestService.AllGuests());
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _guestService.DeleteGuest(id);
            ViewBag.Message = $"The guest with number {id} has deleted succesfully!";
            ViewBag.Succes = 1;
            return View(nameof(Index), await _guestService.AllGuests());
        }
    }
}
