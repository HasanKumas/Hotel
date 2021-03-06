﻿namespace Hotel.Data.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}