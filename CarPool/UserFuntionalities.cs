using System;
using CarPool.Services;
using CarPool.Models;

namespace CarPool
{
    public class UserFunctionalities : Helper
    {
        public int Choice, UserId;

        string phoneNumber, confirmPassword, password, petName;

        IUserServices userServices;

        public User User { get; set; }

        public UserFunctionalities()
        {
            userServices = new UserServices();

            this.User = new User();
        }

        public void SignUp()
        {
            //Getting User details
            User.UserName = GetStringMatch("Please Enter your Name: ", "Name should not be empty", Patterns.Text);
            do
            {
                User.PhoneNumber = GetStringMatch("Please Enter your phone number: ", "Invalid phone number", Patterns.PhoneNumber);
                if (!userServices.IsExistingUser(User.PhoneNumber))
                {
                    break;
                }
                Console.WriteLine("An User with this phone number already exist.Try anthor number");
            } while (true);
            User.EmailAddress = GetStringMatch("Please Enter your email address: ", "Invalid email address", Patterns.EmailAddress);
            Console.Write("Please Enter your Address:");
            User.Address = Console.ReadLine();
            Choice = Convert.ToInt16(GetStringMatch("Please Select your Gender by entering your Choice\n1.Male\n2.Female\n3.Others\n", "Please enter a valid option", @"^[1-3]"));
            User.Gender = Choice == 1 ? "Male" : Choice == 2 ? "Female" : "Other";
            User.Password = GetStringMatch("Please set your password: ", "your password is week.try anthor password", Patterns.Password);
            confirmPassword = GetStringMatch("Confirm your password:", "password and confirm password does not match", User.Password);
            Console.WriteLine("Security Question");
            User.PetName = GetStringMatch("Please Enter your first pet Name: ", "pet name should not be empty", Patterns.Text);
            var registrationChoice = GetStringMatch("Enter 1 to register\nEnter 2 to cancel\n", "please enter a valid option", @"^[1-2]");
            if (Convert.ToInt16(registrationChoice) == 1)
            {
                try
                {
                    userServices.SignUp(User);
                    Console.Clear();
                    Console.WriteLine("Your account has been sucessfully created.\n Press any key to continue");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadKey();
            }
        }

        public void SignIn()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------Sign In-----------------------------------------\n");
            do
            {
                phoneNumber = GetStringMatch("Please Enter your phone number: ", "Invalid phone number", Patterns.PhoneNumber);
                if (userServices.IsExistingUser(phoneNumber))
                {
                    break;
                }
                Console.WriteLine("Incorrect phone number");
            } while (true);
            do
            {
                Console.WriteLine("Please enter your password/enter * to go back");
                password = Console.ReadLine();
                if (password == "*")
                {
                    return;
                }
                if (userServices.SignIn(phoneNumber, password))
                {
                    UserId = userServices.GetUser(phoneNumber).UserId;
                    UserOptions();
                    break;
                }
                Console.WriteLine("Wrong Password");
            } while (true);
        }

        public void ForgotPassword()
        {
            Console.Clear();
            phoneNumber = GetStringInput("Please enter your Phone Number: ", "Invalid phone number", userServices.IsExistingUser);
            Console.WriteLine("Please enter your first petname");
            petName = Console.ReadLine();
            if (userServices.IsValidPetName(phoneNumber, petName))
            {
                Console.WriteLine("Set your new password");
                password = Console.ReadLine();
                Console.WriteLine("Confirm new password");
                confirmPassword = Console.ReadLine();
                if (password == confirmPassword)
                {
                    userServices.ResetPassword(phoneNumber, password);
                    Console.WriteLine("password sucessfully changed");
                }
            }
            else
            {
                Console.WriteLine("wrong answer\npress any key to go back");
                Console.ReadKey();
            }
        }

        public void UserOptions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options\n" +
                    "1.RideProvider\n" +
                    "2.RideTaker\n" +
                    "3.View profile\n" +
                    "4.logout");
                int.TryParse(Console.ReadLine(), out Choice);
                switch (Choice)
                {
                    case 1:
                        RideProviderFunctionalites rideProviderFunctionalities = new RideProviderFunctionalites(UserId);
                        rideProviderFunctionalities.ProviderOptions();
                        break;
                    case 2:
                        RideTakerFunctionalities rideTakerActions = new RideTakerFunctionalities(UserId);
                        rideTakerActions.RideTakerOptions();
                        break;
                    case 3:
                        DisplayProfile();
                        break;
                    case 4:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invallid Choice");
                        break;
                }
            }
        }

        public void DisplayProfile()
        {
            var user = userServices.GetUser(phoneNumber);
            Console.WriteLine($"Name:{user.UserName}");
            Console.WriteLine($"Phone:{user.PhoneNumber}");
            Console.WriteLine($"Email:{user.EmailAddress}");
            Console.WriteLine($"Gender:{user.Gender}");
            Console.WriteLine($"Address:{user.Address}");
            Console.WriteLine($"Password:{user.Password}");
            Console.Write("Press any key to go back");
            Console.ReadKey();
        }
    }
}
