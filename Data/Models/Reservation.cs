using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Models
{
    public class Reservation
    {
        public Reservation()
        {
            RoomReservations = new List<RoomReservation>();
        }
        public int ReservationId { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public int NumberOfGuests { get; set; }

        public double TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public Guest Guest { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
    }
}