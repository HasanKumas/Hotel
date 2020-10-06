using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using ReservationDetailViewModel = Hotel.Models.ReservationDetailViewModel;
using GuestViewModel = Hotel.Models.GuestViewModel;
using RoomDetailViewModel = Hotel.Models.RoomDetailViewModel;
using Hotel.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Hotel.Business;
using Hotel.Data;

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
        /******************************************************************************************************************
            STEPS FOR MAKING A RESERVATION:
            1. GET AVAILABLE Rooms
            2. Check Guest: if not registered create a new guest
            3. Confirm Reservation Details
            4. Complete Reservation
         *******************************************************************************************************************/
        /*1. GET AVAILABLE Rooms
         *******************************************************************************************************************/
        public IActionResult CheckAvailableRooms(ReservationDetailViewModel currentReservation)
        {
            IList<RoomDetailViewModel> availableRooms = _reservationService.GetAvailableRooms(currentReservation).Result;
            if (availableRooms.Count > 0)
            {
                ViewBag.Message = "There are available rooms for your criteria. Please have a look at available rooms below!";
                currentReservation.Rooms.AddRange(availableRooms);
            }
            else
            {
                IList<RoomDetailViewModel> alternateRooms = _reservationService.GetAlternateRooms(currentReservation).Result;
                if (alternateRooms.Count > 0)
                {
                    ViewBag.Message = "There are no available rooms for your criteria. Please have a look at alternate rooms below! You may choose multiple rooms.";
                    currentReservation.Rooms.AddRange(alternateRooms);                    
                }
                else
                {
                    ViewBag.Message = "There are no available rooms for your criteria. Please  search for another period!";
                }
            }
            return View(nameof(ShowAvailableRooms), currentReservation);
        }

        private object ShowAvailableRooms()
        {
            return View();
        }

        /*******************************************************************
         * 2.Check Guest: if already registered get guest details from db
         *  if not registered get details from guest 
        *********************************************************************/
        public async Task<IActionResult> CheckGuest(int[] selectedRoomIDs, /*string lastName,*/ ReservationDetailViewModel currentReservation)
        {
            //Add selected rooms to current reservation
            foreach (var id in selectedRoomIDs)
            {
                currentReservation.Rooms.Add(await _roomService.GetRoom(id));
            }
            //calculate the total price
            currentReservation.TotalPrice = Calculation.CalculatePrice(currentReservation);

            var guest = await _guestService.GetGuestByName(currentReservation.Guest.LastName);
            //if guest already registered
            if (guest != null)
            {
                ViewBag.Message = "Guest is already registered. Please update guest details if necessary.";
                currentReservation.Guest = guest;
                return View(nameof(ConfirmReservation), currentReservation);
            }
            //if guest not registered to create a new guest get details from guest
            else
            {
                ViewBag.Message = "Guest is not already registered. Please fill guest details.";
                //currentReservation.Guest = new GuestViewModel { LastName = lastName };
                return View(nameof(ConfirmReservation), currentReservation);
            }

        }

        /**********************************
         * 3. Confirm Reservation Details
         **********************************/
        private object ConfirmReservation(ReservationDetailViewModel currentReservation)
        {
            return View(currentReservation);
        }

        /*****************************
         * 4. Complete Reservation
         *****************************/
        [HttpPost]
        public async Task<IActionResult> Complete(ReservationDetailViewModel currentReservation)
        {
            bool isReserved;
            
            if (currentReservation.Guest.GuestId > 0)
            {
                _guestService.EditGuest(currentReservation.Guest);
                isReserved = _reservationService.AddReservation(currentReservation);                
            }
            else 
            {
                var newGuestId = _guestService.AddGuest(currentReservation.Guest);
                var newGuest = await _guestService.GetGuest(newGuestId);
                currentReservation.Guest = newGuest;
                isReserved = _reservationService.AddReservation(currentReservation);
            }
            if (isReserved)
            {
                //ViewBag.Message = "Reservation completed succesfully!";
                ViewBag.Succes = 1;
            }
            else
            {
                ViewBag.Message = "Reservation failed!";
                ViewBag.Succes = 1;
            }
            return View(nameof(Index), await _reservationService.AllReservations());
        }

               
        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            ViewBag.Succes = 0;
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

        // POST: Reservation/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReservationDetailViewModel reservation)
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
                ViewBag.Message = $"The reservation with number {id} has updated succesfully!";
                ViewBag.Succes = 1;
                return View(nameof(Index), await _reservationService.AllReservations());
            }
            return View(reservation);
        }

        // GET: Reservation/Delete/5
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

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _reservationService.DeleteReservation(id);
            ViewBag.Message = $"The reservation with number {id} has cancelled succesfully!";
            ViewBag.Succes = 1;
            return View(nameof(Index), await _reservationService.AllReservations());
        }
    }
}
