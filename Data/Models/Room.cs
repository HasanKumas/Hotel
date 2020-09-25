using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data
{
    public class Room
    {
        public Room()
        {
            RoomReservations = new List<RoomReservation>();
        }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public RoomSize RoomSize { get; set; }
        public double Price { get; set; }

        [Display(Name = "Available Date")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EntranceAvailableDate { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
        public List<Maintenance> Maintenances { get; set; }
    }
}