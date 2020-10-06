﻿using System;

namespace Hotel.Data.Models
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room Room { get; set; }
    }
}