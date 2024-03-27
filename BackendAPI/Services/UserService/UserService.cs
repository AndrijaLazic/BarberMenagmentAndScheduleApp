using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using System.Security.Cryptography;

namespace BackendAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private UserContext _userContext;
        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ServiceResponse<User>> RegisterUser(RegistrationDTO registrationDTO)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();

            try
            {
                User user = new User(
                        registrationDTO.Name,
                        registrationDTO.Email,
                        registrationDTO.PhoneNumber);

                if (CheckIfUserExists(user))
                {
                    throw new Exception("UserAlreadyExists");
                }

                CreatePasswordHash(registrationDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                _userContext.Users.Add(user);

                _userContext.SaveChanges();
                response.Data = user;

            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        private bool CheckIfUserExists(User newUser)
        {

            try
            {
                User? oldUser = _userContext.Users.Where(user =>
                    user.Email.Equals(newUser.Email) ||
                    user.PhoneNumber.Equals(newUser.PhoneNumber)
                ).FirstOrDefault();
                if (oldUser == null)
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
