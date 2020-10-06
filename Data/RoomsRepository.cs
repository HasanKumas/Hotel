using Hotel.Data.Models;
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
            _context.Rooms.Add(room);
            return _context.SaveChanges() == 1;
        }
        //GET All Rooms list
        public async Task<IList<Room>> AllRooms()
        {
            return await _context.Rooms.Include(room => room.Maintenances).Include(room => room.RoomReservations).
                                                ThenInclude(rres => rres.Reservation).ToListAsync(); 
        }
        //GET Room Details
        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms.Include(room => room.Maintenances).Include(room => room.RoomReservations).ThenInclude(reservation => reservation.Reservation).FirstOrDefaultAsync(m => m.RoomId == id);
        }
        //GET Room Details
        public async Task<Room> GetRoomByNumber(string roomNumber)
        {
            return await _context.Rooms.Include(room => room.Maintenances).Include(room => room.RoomReservations).ThenInclude(reservation => reservation.Reservation).FirstOrDefaultAsync(m => m.RoomNumber.Equals(roomNumber));
        }
        //POST Update Room
        public bool EditRoom(Room room)
        {
            try
            {
                _context.Update(room);
                return _context.SaveChanges() > 0;
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
            return _context.Rooms.Any(e => e.RoomId == id);
        }

        public bool DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            _context.Rooms.Remove(room);
            return _context.SaveChanges() > 0;
        }

        //public IList<RoomDetailViewModel> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        //{
        //    var rooms = _context.Room.Where(room => room.RoomSize.Equals(numberOfGuests))
        //        .Where(room => room.EntranceAvailableDate <= checkInDate)
        //        .Where(room => room.RoomReservations == null || 
        //            room.RoomReservations.All(reservation => reservation.Reservation.CheckInDate >= checkOutDate) ||
        //            room.RoomReservations.Any(reservation => reservation.Reservation.CheckOutDate <= checkInDate))
        //        .ToList();
        //    return rooms;
        //}
    }
}
