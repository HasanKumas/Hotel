using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Data
{
    public interface IReservationsRepository
    {
        Task<IList<Reservation>> AllReservations();

        bool AddReservation(Reservation room);

        Task<Reservation> GetReservation(int id);
        bool EditReservation(Reservation room);
        bool DeleteReservation(int id);
    }
}
