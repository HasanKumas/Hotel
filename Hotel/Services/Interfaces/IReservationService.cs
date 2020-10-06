using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IList<ReservationDetailViewModel>> AllReservations();

        bool AddReservation(ReservationDetailViewModel reservation);

        Task<ReservationDetailViewModel> GetReservation(int id);
        bool EditReservation(ReservationDetailViewModel reservation);
        bool DeleteReservation(int id);
        Task<IList<RoomDetailViewModel>> GetAvailableRooms(ReservationDetailViewModel reservation);
        Task<IList<RoomDetailViewModel>> GetAlternateRooms(ReservationDetailViewModel currentReservation);
    }
}
