using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using BackendAPI.Models;

namespace BackendAPI.Services.WorkerService
{
    public interface IWorkerService
    {
        Task<ServiceResponse<bool>> RegisterWorker(WorkerRegistrationDTO registrationDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
    }
}
