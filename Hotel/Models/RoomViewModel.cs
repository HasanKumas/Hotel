using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class RoomViewModel
    {
        [Display(Name = "Room ID")]
        public int RoomId { get; set; }
        [Display(Name = "Room Number")]
        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }
        [Display(Name = "Type")]
        [Required]
        public RoomTypeViewModel RoomType { get; set; }
        [Display(Name = "Size")]
        [Required]
        public RoomSizeViewModel RoomSize { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Available Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EntranceAvailableDate { get; set; }
        public RoomStatusViewModel Status { get; set; }

    }
}
