using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public User(string name, string email, string password, string phoneNumber)
        {
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
    }
}
