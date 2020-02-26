using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IRideProviderServices : IRideServices
    {
        List<Ride> GetPastRideOffers(int userId);

        bool AddRide(Ride ride);

        List<Ride> GetAvailableRideOffers(int userId);

        bool ApproveBooking(int bookingId, BookingStatus value);

        bool AddCar(Car car);

        bool IsCarLinked(int providerId);

        List<Car> GetCarsOfUser(int userId);

        List<Booking> GetBookingsForRide(int rideId);

        List<Booking> GetNewBookingRequests(int rideId);

        List<Booking> GetAllBookings(int userId);
    }
}
