using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using Carpool.Data;
using Carpool.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Services
{
    public class RideProviderServices : RideServices, IRideProviderServices
    {
        readonly Repository repository;

        public RideProviderServices()
        {
            repository = new Repository();
        }

        public bool AddRide(Ride ride)
        {
            if (GenericValidator.Validate(ride, out List<string> errors))
            {
                ride.EndTime = ride.StartTime.AddSeconds(GetDurationBetweenPlaces(ride.Source, ride.Destination));
                repository.Add<Rides>(ride.Map<Rides>());
                return true;
            }
            else
            {
                throw new ValidationException(string.Join("\n", errors));
            }
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            return repository.GetAll< Rides>(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date).MapCollection< Rides, Ride>().ToList();
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return repository.GetAll< Rides>(r => r.RideProviderId == userId && r.DateOfRide.Date >= DateTime.Now.Date).MapCollection< Rides, Ride>().ToList();
        }

        public bool ApproveBooking(int bookingId, BookingStatus value)
        {
            Bookings bookings = repository.Get<Bookings>(b => b.BookingId == bookingId);
            Booking booking= bookings.Map<Booking>();
            var currentRide = repository.Get<Rides>(r => r.RideId == booking.RideId).Map<Ride>();

            if (booking != null)
            {
                var route = currentRide.ViaPlaces;
                route.Insert(0, currentRide.Source);
                route.Insert(route.Count(), currentRide.Destination);
                var maxSeatsAvailable = currentRide.NoOfSeatsAvailable;
                bool isPickUpLocationExist = false;
                foreach (var place in route)
                {
                    if (place == booking.Destination)
                    {
                        if (maxSeatsAvailable >= booking.NumberSeatsSelected)
                        { 
                            var bookingModel = repository.Get< Bookings>(b => b.BookingId == bookingId).Map<Booking>();
                            repository.dbContext.Entry(bookings).State = EntityState.Detached;
                            bookingModel.Status = value;
                            repository.Update<Bookings>(bookingModel.Map< Bookings>());
                            repository.dbContext.Entry(bookingModel).State = EntityState.Detached;
                            return true;
                        }
                        break;
                    }
                    var seatsBeingFilled = repository.GetAll< Bookings>(b => b.RideId == currentRide.RideId && b.Source == place && b.Status == (int)BookingStatus.Approved).Select(b => b.NumberSeatsSelected).Sum();
                    var seatsBeingEmpty = repository.GetAll< Bookings>(b => b.RideId == currentRide.RideId && b.Destination == place && b.Status == (int)BookingStatus.Approved).Select(b => b.NumberSeatsSelected).Sum();
                    maxSeatsAvailable -= seatsBeingFilled;
                    maxSeatsAvailable += seatsBeingEmpty;
                    if (isPickUpLocationExist && maxSeatsAvailable <= currentRide.NoOfSeatsAvailable)
                    {
                        if (maxSeatsAvailable > 0)
                        {
                            currentRide.NoOfSeatsAvailable = maxSeatsAvailable;
                        }

                    }
                    if (place == booking.Source && maxSeatsAvailable > 0)
                    {
                        isPickUpLocationExist = true;
                    }
                }
            }
            return false;
        }

        public bool AddCar(Car car)
        {
            var errors = new List<string>();
            if (GenericValidator.Validate(car, out List<string> errorList))
            {
                repository.Add< Cars>(car.Map< Cars>());
                return true;
            }
            else
            {
                throw new ValidationException(string.Join("\n", errorList));
            }
        }

        public bool IsCarLinked(int providerId)
        {
            return repository.Count< Cars>(c => c.OwnerId == providerId) != 0;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return repository.GetAll< Cars>(c => c.OwnerId == userId).MapCollection< Cars, Car>().ToList();
        }

        public List<Booking> GetBookingsForRide(int rideId)
        {
            return repository.GetAll< Bookings>(b => b.RideId == rideId).MapCollection< Bookings, Booking>().ToList();
        }

        // Returns all the bookings get by the user
        public List<Booking> GetAllBookings(int userId)
        {
            var ridesList = repository.GetAll< Rides>(r => r.RideProviderId == userId).ConvertAll(r => r.RideId);
            return repository.GetAll< Bookings>(b => ridesList.Contains(b.RideId)).MapCollection< Bookings, Booking>().ToList();
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return repository.GetAll< Bookings>(b => b.RideId == rideId && b.Status == (short)BookingStatus.Pending).MapCollection< Bookings, Booking>().ToList();
        }
    }
}
