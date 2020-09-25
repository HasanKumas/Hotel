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
            _context.Attach(reservation.Guest);
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
