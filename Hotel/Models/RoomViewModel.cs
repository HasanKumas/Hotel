using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public RoomTypeViewModel RoomType { get; set; }
        public RoomSizeViewModel RoomSize { get; set; }
        public double Price { get; set; }

        [Display(Name = "Available Date")]
        
        [DataType(DataType.Date)]
        public DateTime EntranceAvailableDate { get; set; }
        
        public List<MaintenanceViewModel> Maintenances { get; set; }

    }
}
