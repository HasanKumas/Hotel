using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data
{
    public class Reservation
    {
        public Reservation()
        {
            RoomReservations = new List<RoomReservation>();
        }
        [Display(Name = "Reservation Number")]
        public int ReservationId { get; set; }

        [Display(Name = "CheckIn Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "CheckOut Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public Guest Guest { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
    }
}