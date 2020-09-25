using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class GuestsRepository : IGuestsRepository
    {
        private readonly HotelContext _context;
        public GuestsRepository(HotelContext context)
        {
            _context = context;
        }
        //Add a new Guest
        public bool AddGuest(Guest guest)
        {
            _context.Guest.Add(guest);
            return _context.SaveChanges() == 1;
        }
        //GET All Guests list
        public async Task<IList<Guest>> AllGuests()
        {
            return await _context.Guest.ToListAsync();
        }
        //GET Guest Details
        public async Task<Guest> GetGuest(int id)
        {
            return await _context.Guest.FirstOrDefaultAsync(m => m.GuestId == id);
        }
        //POST Update Guest
        public bool EditGuest(Guest guest)
        {
            try
            {
                _context.Update(guest);
                return _context.SaveChanges() == 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(guest.GuestId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool GuestExists(int id)
        {
            return _context.Guest.Any(e => e.GuestId == id);
        }

        public bool DeleteGuest(int id)
        {
            var guest = _context.Guest.Find(id);
            _context.Guest.Remove(guest);
            return _context.SaveChanges() == 1;
        }
    }
}

