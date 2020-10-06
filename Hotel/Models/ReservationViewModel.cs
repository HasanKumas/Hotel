using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class ReservationViewModel
    {
        [Display(Name = "RNumber")]
        public int ReservationId { get; set; }

        [Display(Name = "CheckIn Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "CheckOut Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Guests")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }
        public ReservationStatusViewModel Status { get; set; }
        public bool IsPaid { get; set; }
        public GuestViewModel Guest { get; set; }
        
    }
}
