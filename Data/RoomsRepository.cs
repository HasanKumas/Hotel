using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly HotelContext _context;
        public RoomsRepository(HotelContext context)
        {
            _context = context;
        }
        //Add a new Room
        public bool AddRoom(Room room)
        {
            _context.Room.Add(room);
            return _context.SaveChanges() == 1;
        }
        //GET All Rooms list
        public async Task<IList<Room>> AllRooms()
        {
            return await _context.Room.ToListAsync();
        }
        //GET Room Details
        public async Task<Room> GetRoom(int id)
        {
            return await _context.Room.FirstOrDefaultAsync(m => m.RoomId == id);
        }
        //POST Update Room
        public bool EditRoom(Room room)
        {
            try
            {
                _context.Update(room);
                return _context.SaveChanges() == 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(room.RoomId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.RoomId == id);
        }

        public bool DeleteRoom(int id)
        {
            var room = _context.Room.Find(id);
            _context.Room.Remove(room);
            return _context.SaveChanges() == 1;
        }
    }
}
