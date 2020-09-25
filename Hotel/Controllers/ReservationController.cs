using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using ReservationDetailViewModel = Hotel.Models.ReservationDetailViewModel;
using GuestViewModel = Hotel.Models.GuestViewModel;
using RoomDetailViewModel = Hotel.Models.RoomDetailViewModel;
using Hotel.Models;

namespace Hotel
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IGuestService _guestService;

        public ReservationController(IReservationService reservationService, IRoomService roomService, IGuestService guestService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _guestService = guestService;
        }
        //GET Complete Reservation

        public IActionResult Complete()
        {
            ViewBag.Message = "1";
            return RedirectToAction("Create");
        }
        //GET AVAILABLE Rooms

        public async Task<IActionResult> CheckAvailableRooms()
        {
            return View(await _roomService.AllRooms() );
        }
        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            return View(await _reservationService.AllReservations());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ReservationDetailViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                //GuestViewModel guest = await _guestService.GetGuest(reservation.Guest.GuestId);
                //RoomDetailViewModel room = await _roomService.GetRoom(reservation.Rooms[0].RoomId);
                //reservation.Guest = guest;
                //reservation.Rooms.Add(room);

                _reservationService.AddReservation(reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation((int)id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        //// POST: Reservation/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, ReservationDetailViewModel reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool res = _reservationService.EditReservation(reservation);
                if (!res)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        //// GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        //// POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
