﻿using Domain.Models;
using Domain.Models.DTO;
using Domain.Models.Database;

namespace BLL.Services
{
    public interface IWorkerService
    {
        Task<ServiceResponse<bool>> RegisterWorker(WorkerRegistrationDTO registrationDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);

        Task<ServiceResponse<List<WorketDTO>>> GetWorkers();

        Task<ServiceResponse<List<MessageDTO>>> GetChatMessages(int chatId);

        Task<ServiceResponse<WorkerCommunication>> GetChat(int userId, int secondUserId);

        Task<ServiceResponse<WorkerCommunication>> CreateWorkerChat(int User1Id, int User2Id);

        Task<ServiceResponse<int>> PostMessage(string Message, int SenderID);

    }
}
