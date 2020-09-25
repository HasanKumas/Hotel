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
        bool EditRoom(Room room);
        bool DeleteRoom(int id);
    }
}
