using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Models
{
    public class Ride
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RideId { get; set; }

        //[AvailableSeats]
        public int NoOfSeatsAvailable { get; set; }

        [Required(ErrorMessage ="Strating time should not be empty")]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required(ErrorMessage ="Starting location can not be empty")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination location can not be empty")]
        public string Destination { get; set; }

        [Required(ErrorMessage ="Car number should not be empty")]
        public string CarNumber { get; set; }

        public List<string> ViaPlaces { get; set; }

        public int RideProviderId { get; set; }

        [Required(ErrorMessage ="Cost per killometer should not be empty")]
        [Range(0.1,20,ErrorMessage ="You can charge upto 20 rupees per killometer only")]
        public decimal PricePerKilometer { get; set; }

        [Required(ErrorMessage ="Date of ride should not be empty")]
        [PresentOrFutureDate(ErrorMessage ="PastDate not allowed")]
        public DateTime DateOfRide { get; set; }

        public Ride(int id,int rideProviderID,string carNumber, string source, string destination, DateTime startTime, DateTime endTime, int noOfSeats, List<string> viaPlaces, decimal amount,DateTime dateOfRide)
        {
            this.RideId = id;
            this.RideProviderId = rideProviderID;
            this.CarNumber = carNumber;
            this.Source = source;
            this.Destination = destination;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.NoOfSeatsAvailable = noOfSeats;
            this.ViaPlaces = viaPlaces;
            this.PricePerKilometer = amount;
            this.DateOfRide = dateOfRide;
        }
        public Ride()
        {

        }
    }
}
