using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    public interface IRideTakerServices : IRideServices
    {
        bool BookRide(Booking booking);

        List<Booking> GetAllBookings(int userId);

        List<Ride> GetAllRideOffers(int userId);

        List<Ride> SearchRides(string pickupLocation, string dropLocation, int userId);

        Car GetCarDetails(string carNumber);
    }
}
