using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public interface IGuestsRepository
    {
        Task<IList<Guest>> AllGuests();

        bool AddGuest(Guest guest);

        Task<Guest> GetGuest(int id);
        bool EditGuest(Guest guest);
        bool DeleteGuest(int id);
    }
}
