using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        [Required]
        public string PetName { get; set; }

        [StringLength(maximumLength:16 ,MinimumLength =4,ErrorMessage ="Password Length should consists of minimum 4 characters")]
        public string Password { get; set; }

        public User(int userId, string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.PhoneNumber = phoneNumber;
            this.EmailAddress = emailAddress;
            this.Address = address;
            this.Gender = gender;
            this.Password = password;
            this.PetName = petName;
        }

        public User()
        {
            
        }
    }
}
