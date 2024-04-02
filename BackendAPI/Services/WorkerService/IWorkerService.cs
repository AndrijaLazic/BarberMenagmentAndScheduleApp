using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using BackendAPI.Models;
using BackendAPI.Models.Socket;

namespace BackendAPI.Services.WorkerService
{
    public interface IWorkerService
    {
        Task<ServiceResponse<bool>> RegisterWorker(WorkerRegistrationDTO registrationDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);

        Task<ServiceResponse<List<Worker>>> GetWorkers();

        Task<ServiceResponse<List<WorkerMessage>>> GetChatMessages(int chatId);

        Task<ServiceResponse<WorkerCommunication>> GetChat(string JWT, int secondUserId);
    }
}
