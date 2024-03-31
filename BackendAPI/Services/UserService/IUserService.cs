using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;

namespace BackendAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<bool>> RegisterUser(RegistrationDTO registrationDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
    }
}
