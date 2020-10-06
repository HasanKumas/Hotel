using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Models
{
    public class Room
    {
        public Room()
        {
            RoomReservations = new List<RoomReservation>();
            Maintenances = new List<Maintenance>();
        }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public RoomSize RoomSize { get; set; }
        public double Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime EntranceAvailableDate { get; set; }
        public RoomStatus Status { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
        public List<Maintenance> Maintenances { get; set; }
    }
}