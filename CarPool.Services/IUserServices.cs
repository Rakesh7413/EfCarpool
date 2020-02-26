using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IUserServices
    {
        bool SignIn(string phoneNumber, string password);

        bool SignUp(User user);

        User GetUser(int userId);

        bool IsExistingUser(string phoneNumber);

        bool IsValidPetName(string phoneNumber, string petName);

        void ResetPassword(string phoneNumber, string password);

        User GetUser(string phoneNumber);

    }
}
