using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class ReservationDetailViewModel : ReservationViewModel
    {
        public ReservationDetailViewModel()
        {
            Rooms = new List<RoomDetailViewModel>();
        }
        public List<RoomDetailViewModel> Rooms { get; set; }
    }
}
