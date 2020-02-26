using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;

namespace CarPool
{
    public class RideTakerFunctionalities
    {
        int Choice, userId;

        IRideTakerServices rideTakerServices;

        IUserServices userServices;

        public RideTakerFunctionalities(int userId)
        {
            rideTakerServices = new RideTakerServices();

            userServices = new UserServices();

            this.userId = userId;
        }

        public void RideTakerOptions()
        {
            // Displays the options for ridetaker
            bool repeat = true;

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options\n" +
                    "1.view Ride Offers\n" +
                    "2.Book a Ride\n" +
                    "3.View Bookings\n" +
                    "4.Go Back\n" +
                    "5.Logout\n");
                int.TryParse(Console.ReadLine(), out Choice);

                switch (Choice)
                {
                    case 1:
                        DisplayAllRideOffers();
                        break;
                    case 2:
                        BookRide();
                        break;
                    case 3:
                        ViewBookings();
                        break;
                    case 4:
                        repeat = false;
                        break;
                    case 5:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid option\n please choose a valid option");
                        break;
                }
            }
        }

        private void ViewBookings()
        {
            var bookings = rideTakerServices.GetAllBookings(userId);
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops! there are no booking requests to show");
            }
            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"\n-------------------------------------------------------------------------------------------------------------\n" +
                    $"{i + 1}.Booking Date:{bookings[i].BookingDate.ToShortDateString()}\nFrom:{bookings[i].Source}\t\tTo:{bookings[i].Destination}\n" +
                    $"Journey Time:{bookings[i].StartTime.ToShortTimeString()}-{bookings[i].EndTime.ToShortTimeString()}\n" +
                    $"Status:{Enum.GetName(typeof(BookingStatus), bookings[i].Status)}\tCost:{bookings[i].CostOfBooking}");
            }
            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------\n" +
                $"Press any key to go back");
            Console.ReadKey();
        }

        private void BookRide()
        {
            var helper = new Helper();

            int noOfSeatsSelected;

            Console.Clear();
            Console.WriteLine("please Enter pick up location:");

            string pickupLocation = Enum.GetName(typeof(Places), helper.GetUserChoiceInEnum<Places>());

            Console.WriteLine("please Enter Drop location");

            string dropLocation = Enum.GetName(typeof(Places), helper.GetUserChoiceInEnum<Places>());

            var availableRides = rideTakerServices.SearchRides(pickupLocation, dropLocation, userId);

            if (availableRides.Count == 0)
            {
                Console.WriteLine("No rides Available to show\npress any key to continue");
                Console.ReadKey();
            }
            else
            {
                DisplayRideOffers(availableRides, pickupLocation, dropLocation);
                Choice = helper.GetIntegerInRange(minimumValue: 0, maximumValue: availableRides.Count,
                                    displayMessage: "Please enter the ride number you want to make booking /Enter '0' to go back: ", errorMessage: "Invalid ride number");
                if (Choice == 0)
                {
                    return;
                }
                else
                {
                    noOfSeatsSelected = helper.GetIntegerInRange(displayMessage: "Enter number of seats you want to book: ", errorMessage: "Invalid number of seats",
                    minimumValue: 1, maximumValue: availableRides[Choice - 1].NoOfSeatsAvailable);
                }

                var bookingChoice = helper.GetIntegerInRange(displayMessage: "Enter 1 to confirm Booking.\nEnter 2 to cancel Booking", errorMessage: "Invalid option", minimumValue: 1, maximumValue: 2);
                if (Choice == 1)
                {
                    if (rideTakerServices.BookRide(new Booking(availableRides[Choice - 1].RideId, pickupLocation, dropLocation, noOfSeatsSelected, userId)))
                    {
                        Console.Clear();
                        Console.WriteLine("Request Sent sucessfully");
                    }
                    else
                    {
                        Console.WriteLine("Request can not be sent");
                    }
                    Console.ReadKey();
                }
            }
        }

        private void DisplayRideOffers(IList<Ride> rideOffers, string source = null, string destination = null)
        {
            int index = 1;

            decimal costOfRide;

            if (rideOffers.Count == 0)
            {
                Console.WriteLine("No rides Available to show\nPress any key");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
                foreach (Ride r in rideOffers)
                {

                    Console.WriteLine($"Ride number:{index++}\tRideId:{r.RideId}\t\tRideProvide Name:{userServices.GetUser(r.RideProviderId).UserName}" +
                        $"\nFrom: { r.Source}\t\tTo: {r.Destination}");
                    Console.Write("Via:|");
                    if (r.ViaPlaces != null)
                    {
                        foreach (string place in r.ViaPlaces)
                        {
                            Console.Write(place + "|");
                        }
                    }

                    var currentCar = rideTakerServices.GetCarDetails(r.CarNumber);

                    Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\nSeats Available={r.NoOfSeatsAvailable}\n" +
                        $"Journey Date:{r.DateOfRide.ToShortDateString()}\nCar Details:\nCar type:{currentCar.Capacity - 1}\t" +
                        $"CarNumber:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");
                    source = source != null ? source : r.Source;
                    destination = destination != null ? destination : r.Destination;
                    costOfRide = rideTakerServices.GetDistanceBetweenPlaces(source, destination) * r.PricePerKilometer;
                    Console.WriteLine($"Cost:{costOfRide}");
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
                }
            }
        }

        private void DisplayAllRideOffers()
        {
            Console.Clear();
            var rideOffers = rideTakerServices.GetAllRideOffers(userId);
            DisplayRideOffers(rideOffers);
            Console.WriteLine("Press any key to GoBack");
            Console.ReadKey();
        }
    }
}
