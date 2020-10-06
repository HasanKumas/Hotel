using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class GuestViewModel
    {
        public int GuestId { get; set; }
        [Display(Name = "Surname"), Required]
        public string LastName { get; set; }
        [Display(Name = "Name"), Required]
        public string FirstName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Display(Name = "City")]
        public string Town { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email"), Required]
        public string Email { get; set; }

    }
}
