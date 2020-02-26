using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.Data;
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
            CurrentUser = repository.Get<Data.Models.User>(u => u.PhoneNumber == phoneNumber).Map<User>();
            return (CurrentUser != null && CurrentUser.Password == password);
        }
        public User GetUser(int userId)
        {
            return repository.Get<Data.Models.User>(u => u.UserId == userId).Map<User>();
        }
        public bool IsExistingUser(string phoneNumber)
        {
            return repository.Get<Data.Models.User>(u => u.PhoneNumber == phoneNumber).Map<User>() != null;
        }
        public bool SignUp(User user)
        {
            if (GenericValidator.Validate(user, out List<string> errors))
            {
                repository.Add<Data.Models.User>(MapperHelper.Map<Data.Models.User>(user)); //is it the correct usage?
                return true;
            }
            else
            {
                throw new ValidationException(string.Join("\n", errors));
            }
        }

        public bool IsValidPetName(string phoneNumber, string petName)
        {
            return repository.Get<Data.Models.User>(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            var user = repository.Get<Data.Models.User>(u => u.PhoneNumber == phoneNumber);
            user.Password = password;
            repository.Update<Data.Models.User>(user);
        }

        public User GetUser(string phoneNumber)
        {
            return repository.Get<Data.Models.User>(u => u.PhoneNumber == phoneNumber).Map<User>();
        }

    }
}
