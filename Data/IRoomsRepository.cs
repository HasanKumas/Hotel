using Hotel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public interface IRoomsRepository
    {
        Task<IList<Room>> AllRooms();

        bool AddRoom(Room room);

        Task<Room> GetRoom(int id);
        Task<Room> GetRoomByNumber(string roomNumber);
        bool EditRoom(Room room);
        bool DeleteRoom(int id);
        //IList<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests);
    }
}
