using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class Bookings
    {
        public int BookingId { get; set; }
        public int RideId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public short Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal CostOfBooking { get; set; }
        public int NumberSeatsSelected { get; set; }
    }
}
