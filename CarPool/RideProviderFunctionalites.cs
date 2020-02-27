using System;
using System.Collections.Generic;
using System.Globalization;
using CarPool.Models;
using CarPool.Services;

namespace CarPool
{
    public class RideProviderFunctionalites : Helper
    {
        public int Choice, capacity, providerId, carType;

        public string CarNumber;

        IRideProviderServices rideProviderServices;

        IUserServices userServices;

        public RideProviderFunctionalites(int providerId)
        {
            rideProviderServices = new RideProviderServices();

            userServices = new UserServices();

            this.providerId = providerId;
        }

        public void ProviderOptions()
        {
            bool repeat = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options\n" +
                    "1.Create New Ride Offer\n" +
                    "2.Created Ride Offers\n" +
                    "3.Past Rides\n" +
                    "4.Get All Booking requests\n" +
                    "5.Go Back\n" +
                    "6.logout");

                int.TryParse(Console.ReadLine(), out Choice);
                switch (Choice)
                {
                    case 1:
                        CreateRideOffer();
                        break;
                    case 2:
                        AvailableRideOffers();
                        break;
                    case 3:
                        ShowPastRideOffers();
                        break;
                    case 4:
                        DisplayAllBookingRequests();
                        break;
                    case 5:
                        repeat = false;
                        break;
                    case 6:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:
                        Console.WriteLine("In correct option\n please choose a valid option");
                        break;
                }
            } while (repeat);
        }

        private void DisplayAllBookingRequests()
        {
            var bookingRequests = rideProviderServices.GetAllBookings(providerId);
            if (bookingRequests.Count != 0)
            {
                DisplayBookings(bookingRequests);
            }
            else
            {
                Console.WriteLine("Oops there are no booking requests");
            }
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
        }

        private void ShowPastRideOffers()
        {
            // It will display all ride offers provided by the current user which are completed.
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            var currentRides = rideProviderServices.GetPastRideOffers(providerId);
            if (currentRides.Count != 0)
            {
                foreach (Ride r in currentRides)
                {
                    Console.WriteLine($"{i++}.RideId:{r.RideId}\nFrom:{r.Source}\t\t\tTo:{r.Destination} \nStartTime:{r.StartTime.ToShortTimeString()}\tEndTime:{r.EndTime.ToShortTimeString()}");
                    int j = 1;
                    foreach (Booking booking in rideProviderServices.GetBookingsForRide(r.RideId))
                    {
                        Console.WriteLine($"\t{j++}.{booking.UserId}\t status:{booking.Status}");
                    }
                    if (rideProviderServices.GetBookingsForRide(r.RideId).Count == 0)
                    {
                        Console.WriteLine("Oops.! There are no Bookings for this ride");
                    }
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
                }
            }
            else
            {
                Console.WriteLine("Oops! you have not provided any ride offer to show\n press any key to go back");
            }
            Console.ReadKey();
        }

        private void AvailableRideOffers()
        {
            var currentRides = rideProviderServices.GetAvailableRideOffers(providerId);
            int rideNo;
            Console.Clear();
            if (currentRides.Count == 0)
            {
                Console.WriteLine("No Ride Offers available\n press any key to go back");
                Console.ReadKey();
                return;
            }
            // Displaying the ride offers created by the user that are not completed
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            for (int i = 0; i < currentRides.Count; i++)
            {
                Console.WriteLine($"RideNo:{i + 1}\t\tRidedate:{currentRides[i].DateOfRide.Date.ToShortDateString()}\nFrom:{currentRides[i].Source}\t\tTo:{currentRides[i].Destination} " +
                    $"\nStartTime:{currentRides[i].StartTime.ToShortTimeString()}\tEndTime:{currentRides[i].EndTime.ToShortTimeString()}\n" +
                    $"New Booking Requests:{rideProviderServices.GetNewBookingRequests(currentRides[i].RideId).Count}" +
                    $"\n-----------------------------------------------------------------------------------------------------\n\n");
            }

            while (true)
            {
                rideNo = GetIntegerInRange(minimumValue: 0, maximumValue: currentRides.Count, displayMessage: "please Enter an ride number from above to approve booking requests for that ride/enter 0 to go back",
               errorMessage: "Invalid ride number");
                if (rideNo == 0)
                {
                    return;
                }
                else
                {
                    ApproveBookings(currentRides[rideNo - 1].RideId);
                    break;
                }
            }
        }

        private void DisplayBookings(List<Booking> bookings)
        {
            int i = 1;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------\n");
            foreach (Booking booking in bookings)
            {
                var requester = userServices.GetUser(booking.UserId);
                Console.WriteLine($"Booking number: {i++}.\nRequested BY:{requester.UserName}\t" +
                    $"Phone Number:{requester.PhoneNumber}\nPick Up Location:{booking.Source}\tDrop Location:{booking.Destination}\n" +
                    $"Number of seats requested:{booking.NumberSeatsSelected}\tAmount you will get:Rs.{booking.CostOfBooking}" +
                    $"\n--------------------------------------------------------------------------------------------------------\n");
            }
        }

        private void ApproveBookings(int rideId)
        {
            var bookings = rideProviderServices.GetNewBookingRequests(rideId);
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops.! There are no Bookings for this ride\n press any key to go back");
                Console.ReadKey();
            }
            else
            {
                DisplayBookings(bookings);
                while (true)
                {
                    Console.WriteLine();

                    int bookingNo = GetIntegerInRange("Please choose the booking number you want to approve/reject:", "Invalid booking number", 1, bookings.Count);
                    Choice = Convert.ToInt16(GetStringMatch("Enter 1 to approve\nEnter 2 to reject", "Invalid Option", @"^[12]"));
                    if (Choice == 1)
                    {
                        if (rideProviderServices.ApproveBooking(bookings[bookingNo - 1].BookingId, BookingStatus.Approved))
                        {
                            Console.WriteLine("Request Approved");
                        }
                        else
                        {
                            Console.WriteLine("Request can not be approved");
                        }
                    }
                    else if (Choice == 2)
                    {
                        if (rideProviderServices.ApproveBooking(bookings[bookingNo - 1].BookingId, BookingStatus.Rejected))
                        {
                            Console.WriteLine("Request Rejected");
                        }
                        else
                        {
                            Console.WriteLine("Request can not be Rejected");
                        }
                    }
                    Console.WriteLine("Enter 1 to continue approving");
                    Console.WriteLine("Enter 2 to Go Back");
                    int.TryParse(Console.ReadLine(), out Choice);
                    if (Choice == 2)
                    {
                        break;
                    }
                }
            }

        }

        private void CreateRideOffer()
        {
            Ride ride = new Ride();

            string time;

            int optionNumber = 1;

            List<Car> availableCars;

            Console.Clear();
            // It will check if the user has provided ride offers previously,display list of cars he used previously and ask user to selsct one among them or new car.
            if (rideProviderServices.IsCarLinked(providerId))
            {
                Console.WriteLine("List Of Cars Used:");
                availableCars = rideProviderServices.GetCarsOfUser(providerId);
                Console.WriteLine("Please Choose One of the following cars:");
                for (int i = 0; i < availableCars.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{availableCars[i].CarNo}");
                }
                do
                {
                    Console.WriteLine("Enter your Choice/enter * to add new car ");
                    string userChoice = Console.ReadLine();
                    int.TryParse(userChoice, out Choice);
                    if (userChoice == "*")
                    {
                        AddCar(providerId);
                        break;
                    }
                    else if (Choice > 0 && Choice <= availableCars.Count)
                    {
                        CarNumber = availableCars[Choice - 1].CarNo;
                        capacity = availableCars[Choice - 1].Capacity;
                        carType = (int)availableCars[Choice - 1].CarType;
                        break;
                    }
                } while (true);
            }
            else
            {
                if (!AddCar(providerId))
                {
                    Console.Write("Sorry! you can not create a ride offer.\nPress any key");
                    Console.ReadKey();
                    return;
                }
            }

            var date = GetStringMatch("Enter date of Journey(dd/mm/yyyy):", "Invalid date format", Patterns.Date).Split('/');

            ride.DateOfRide = DateTime.Parse(date[1] + " / " + date[0] + " / " + date[2]);

            Console.WriteLine("\nPlease select Starting Location");

            ride.Source = Enum.GetName(typeof(Places), GetUserChoiceInEnum<Places>());

            time = GetStringMatch("Please enter start time (HH:MM) In 24 Hour Format: ", "Invalid time", Patterns.Time);

            ride.StartTime = DateTime.ParseExact(time + ":00", "HH:mm:ss", CultureInfo.InvariantCulture);

            Console.WriteLine("\nPlease select destination Location");

            ride.Destination = Enum.GetName(typeof(Places), GetUserChoiceInEnum<Places>());

            ride.NoOfSeatsAvailable = GetIntegerInRange("Please enter No of seats available", "Invalid Data", 1, capacity - 1);
            
            Console.WriteLine("Enter Intermediate places seperated by spaces:");

            var places = Enum.GetNames(typeof(Places));

            var viaPlaces = new List<string>();

            foreach (string value in places)
            {
                Console.WriteLine($"{optionNumber++}.{value}");
            }

            foreach (string choice in Console.ReadLine().Split(' '))
            {
                int.TryParse(choice, out int index);
                if (index > 0 && index < places.Length)
                {
                    viaPlaces.Add(places[index - 1]);
                }
            }
            do
            {

                Console.Write($"Maximum fare you can charge is (per KM):{rideProviderServices.GetMaximumCharge(carType)}.\n Do you want to continue with the fare or change the fare(Y/N):");
                switch (Console.ReadLine())
                {
                    case "y":
                        ride.PricePerKilometer = rideProviderServices.GetMaximumCharge(carType);
                        break;
                    case "n":
                        ride.PricePerKilometer = Convert.ToDecimal(GetStringMatch("Please Enter Cost per Kilometer(Rupees.paise): ", "Price cannot be more than the base price.", Patterns.Amount));
                        break;
                    default:
                        Console.WriteLine("Invalid Input.");
                        break;
                }
            } while (rideProviderServices.GetMaximumCharge(carType)<ride.PricePerKilometer);

            ride.CarNumber = CarNumber;

            ride.RideProviderId = providerId;

            ride.ViaPlaces = viaPlaces;

            try
            {
                rideProviderServices.AddRide(ride);
                Console.WriteLine("Ride Added Sucessfully\nPress any key");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private bool AddCar(int providerId)
        {
            CarNumber = GetStringMatch("Enter car number", "car number should not be empty", Patterns.CarNumber);
            var carName = GetStringMatch("Enter the car name", "car name should not be empty", Patterns.Name);
            capacity = Convert.ToInt16(GetStringMatch("Enter Capacity of car[4-8]", "Invalid Capacity", Patterns.CarCapacity));
            Console.WriteLine("Select car type:");
            carType = GetUserChoiceInEnum<CarType>();
            try
            {
                rideProviderServices.AddCar(new Car { CarNo = CarNumber, CarName = carName, Capacity = capacity, CarType = (CarType)carType, OwnerId = providerId });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops! you cant add this car\n{e.Message}");
                return false;
            }
            return true;
        }
    }
}
