using Hotel.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly HotelContext _context;
        public ReservationsRepository(HotelContext context)
        {
            _context = context;
        }
        //Add a new Reservation
        public bool AddReservation(Reservation reservation)
        {
            //Get Guest
            var guest = _context.Guests.Find(reservation.Guest.GuestId);
            reservation.Guest = guest;

            //Get Room
            var rooms = _context.Rooms.ToList().Where(room => reservation.RoomReservations.Any(rr => rr.RoomId == room.RoomId));
            
            for (int i =0; i < rooms.Count(); i++)
            {
                reservation.RoomReservations[i].Room = rooms.ElementAt(i);
              
            }
            
            _context.Reservations.Add(reservation);
            return _context.SaveChanges() > 0;
        }

        //GET All Reservations list
        public async Task<IList<Reservation>> AllReservations()
        {
            return await _context.Reservations.Include(res => res.Guest).
                                                Include(res => res.RoomReservations).
                                                ThenInclude(ress => ress.Room).ToListAsync();
        }
        //GET Reservation Details
        public async Task<Reservation> GetReservation(int id)
        {
            return await _context.Reservations.Include(m => m.RoomReservations).
                                                ThenInclude(mr => mr.Room).
                                                FirstOrDefaultAsync(m => m.ReservationId == id);
        }
        //POST Update Reservation
        public bool EditReservation(Reservation reservation)
        {
            try
            {
                _context.Update(reservation);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.ReservationId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }

        public bool DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            _context.Reservations.Remove(reservation);
            return _context.SaveChanges() > 0;
        }
    }
}
