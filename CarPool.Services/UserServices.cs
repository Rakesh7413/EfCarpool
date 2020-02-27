using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using Carpool.Data;
using Carpool.Data.Models;
using AutoMapper;
using CarPool.Service;

namespace CarPool.Services
{
    public class UserServices : IUserServices
    {

        public User CurrentUser;
        public Repository repository;

        public UserServices()
        {
            repository = new Repository();
        }

        public bool SignIn(string phoneNumber, string password)
        {
            CurrentUser = repository.Get<Users>(u => u.PhoneNumber == phoneNumber).Map<User>();
            return (CurrentUser != null && CurrentUser.Password == password);
        }
        public User GetUser(int userId)
        {
            return repository.Get<Users>(u => u.UserId == userId).Map<User>();
        }
        public bool IsExistingUser(string phoneNumber)
        {
            return repository.Get<Users>(u => u.PhoneNumber == phoneNumber).Map<User>() != null;
        }
        public bool SignUp(User user)
        {
            if (GenericValidator.Validate(user, out List<string> errors))
            {
                repository.Add<Users>(MapperHelper.Map<Users>(user)); 
                return true;
            }
            else
            {
                throw new ValidationException(string.Join("\n", errors));
            }
        }

        public bool IsValidPetName(string phoneNumber, string petName)
        {
            return repository.Get<Users>(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            var user = repository.Get<Users>(u => u.PhoneNumber == phoneNumber);
            user.Password = password;
            repository.Update<Users>(user);
        }

        public User GetUser(string phoneNumber)
        {
            return repository.Get<Users>(u => u.PhoneNumber == phoneNumber).Map<User>();
        }

    }
}
