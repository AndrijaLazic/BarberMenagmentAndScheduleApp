using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;

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
                        registrationDTO.Password,
                        registrationDTO.PhoneNumber);

                if (CheckIfUserExists(user))
                {
                    throw new Exception("UserAlreadyExists");
                }
                    

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
    }
}
