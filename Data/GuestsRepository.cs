using Hotel.Data.Models;
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
        public int AddGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            _context.SaveChanges();
            return guest.GuestId;
        }
        //GET All Guests list
        public async Task<IList<Guest>> AllGuests()
        {
            return await _context.Guests.ToListAsync();
        }
        //GET Guest Details
        public async Task<Guest> GetGuest(int id)
        {
            return await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == id);
        }
        public async Task<Guest> GetGuestByName(string lastName)
        {
            return await _context.Guests.FirstOrDefaultAsync(m => m.LastName == lastName);
        }
        //public async Task<Guest> GetGuestByName(string lastName)
        //{
        //    return await _context.Guest.FirstOrDefaultAsync(m => m.LastName == lastName);
        //}

        //POST Update Guest
        public bool EditGuest(Guest guest)
        {
            try
            {
                _context.Update(guest);
                return _context.SaveChanges() > 0;
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
            return _context.Guests.Any(e => e.GuestId == id);
        }

        public bool DeleteGuest(int id)
        {
            var guest = _context.Guests.Find(id);
            _context.Guests.Remove(guest);
            return _context.SaveChanges() > 0;
        }
    }
}

