using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class ReservationDetailViewModel : ReservationViewModel
    {
        public List<RoomViewModel> Rooms { get; set; }
    }
}
