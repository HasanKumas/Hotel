using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class RoomDetailViewModel : RoomViewModel
    {
        public List<ReservationViewModel> Reservations { get; set; }
    }
}
