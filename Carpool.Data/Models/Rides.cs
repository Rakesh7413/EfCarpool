using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class Rides
    {
        public int RideId { get; set; }
        public int NoOfSeatsAvailable { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string CarNumber { get; set; }
        public int RideProviderId { get; set; }
        public decimal PricePerKilometer { get; set; }
        public DateTime DateOfRide { get; set; }
        public string ViaPlaces { get; set; }
    }
}
