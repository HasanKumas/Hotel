using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class RoomDetailViewModel : RoomViewModel
    {
        public RoomDetailViewModel()
        {
            Reservations = new List<ReservationViewModel>();
            Maintenances = new List<MaintenanceViewModel>();
        }
        public List<ReservationViewModel> Reservations { get; set; }
        public List<MaintenanceViewModel> Maintenances { get; set; }
    }
}
