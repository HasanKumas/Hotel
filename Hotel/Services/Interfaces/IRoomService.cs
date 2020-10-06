using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IList<RoomDetailViewModel>> AllRooms();

        bool AddRoom(RoomDetailViewModel room);

        Task<RoomDetailViewModel> GetRoom(int id);
        bool EditRoom(RoomDetailViewModel room);
        bool DeleteRoom(int id);
        Task<IList<RoomDetailViewModel>> CanBlockedRoomsAsync(string roomNumber, DateTime startDate, DateTime endDate);
        
    }
}
