using Domain.Models;
using Domain.Models.DTO;

namespace BLL.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<bool>> RegisterUser(RegistrationDTO registrationDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
    }
}
