using System;
using System.Collections.Generic;

namespace Carpool.Data.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PetName { get; set; }
        public string Password { get; set; }
    }
}
