using Microsoft.EntityFrameworkCore;
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
            //var newReservation = _context.Reservation.Include(b => b.Guest).First();
            //Get Guest
            var guest = _context.Guest.Find(reservation.Guest.GuestId);
            reservation.Guest = guest;

            //Get Room
            var rooms = _context.Room.ToList().Where(r => reservation.RoomReservations.Any(rr => rr.Room.RoomId == r.RoomId));
            reservation.RoomReservations.Clear();
            foreach (var room in rooms)
            {
                reservation.RoomReservations.Add(new RoomReservation
                {
                    Room = room,
                    Reservation = reservation
                });
            }

            _context.Reservation.Add(reservation);
            return _context.SaveChanges() == 1;
        }
        //GET All Reservations list
        public async Task<IList<Reservation>> AllReservations()
        {
            return await _context.Reservation.Include(res => res.Guest).Include(res => res.RoomReservations).ThenInclude(ress => ress.Room).ToListAsync();
        }
        //GET Reservation Details
        public async Task<Reservation> GetReservation(int id)
        {
            return await _context.Reservation.Include(m => m.RoomReservations).ThenInclude(mr => mr.Room).FirstOrDefaultAsync(m => m.ReservationId == id);
        }
        //POST Update Reservation
        public bool EditReservation(Reservation reservation)
        {
            try
            {
                _context.Update(reservation);
                return _context.SaveChanges() == 1;
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
            return _context.Reservation.Any(e => e.ReservationId == id);
        }

        public bool DeleteReservation(int id)
        {
            var reservation = _context.Reservation.Find(id);
            _context.Reservation.Remove(reservation);
            return _context.SaveChanges() == 1;
        }
    }
}
