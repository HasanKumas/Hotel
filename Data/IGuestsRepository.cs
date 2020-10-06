using Hotel.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public interface IGuestsRepository
    {
        Task<IList<Guest>> AllGuests();

        int AddGuest(Guest guest);

        Task<Guest> GetGuest(int id);
        Task<Guest> GetGuestByName(string lastName);
        bool EditGuest(Guest guest);
        bool DeleteGuest(int id);
    }
}
