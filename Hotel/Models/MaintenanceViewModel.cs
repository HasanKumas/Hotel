using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class MaintenanceViewModel
    {
        public int MaintenanceId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public RoomDetailViewModel Room { get; set; }
    }
}
