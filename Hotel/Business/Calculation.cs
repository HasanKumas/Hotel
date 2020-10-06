using Hotel.Models;
using Hotel.Services;
using System.Collections.Generic;

namespace Hotel.Business
{

    public class Calculation
    {
        private readonly GuestService _guestService;

        public Calculation(GuestService guestService)
        {
            _guestService = guestService;
        }
        public static double CalculatePrice(ReservationDetailViewModel currentReservation)
        {
            double total = 0;
            var checkInDate = currentReservation.CheckInDate;
            var checkOutDate = currentReservation.CheckOutDate;
            var days = (checkOutDate - checkInDate).Days;

            foreach (var room in currentReservation.Rooms)
            {
                total += room.Price * days;
            }
            return total;
        }
        public static int CalculateTotalBeds(List<RoomDetailViewModel> alternateRooms)
        {
            var totalBeds = 0;
            foreach(var room in alternateRooms)
            {
                totalBeds += (int)room.RoomSize; 
            }
            return totalBeds;
        }
    }
}
