using DAL.Data;
using Domain;
using Domain.Models;
using Domain.Models.Database;
using Domain.Models.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace BLL.Services.DataService
{
    public class WorkerService : IWorkerService
    {
        private BarberDBContext _databaseContext;
        private readonly IOptions<AppConfiguration> _options;

        public WorkerService(BarberDBContext userContext, IOptions<AppConfiguration> options)
        {
            _databaseContext = userContext;
            _options = options;
        }

        public async Task<ServiceResponse<bool>> RegisterWorker(WorkerRegistrationDTO registrationDTO)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            Worker worker = new Worker
            {
                Email = registrationDTO.Email,
                Name = registrationDTO.Name,
                PhoneNumber = registrationDTO.PhoneNumber,
                LastName = registrationDTO.LastName,
                WorkerTypeId = registrationDTO.WorkerTypeId
            };

            CreatePasswordHash(registrationDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            worker.PasswordSalt = passwordSalt;
            worker.PasswordHash = passwordHash;

            _databaseContext.Workers.Add(worker);

            _databaseContext.SaveChanges();
            response.Data = true;


            return response;

        }

        public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
        {
            Console.WriteLine(_options.Value.JWTduration);
            ServiceResponse<string> response = new ServiceResponse<string>();

            var user = _databaseContext.Workers.Where(x => x.Email.Equals(loginDTO.Email)).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("EmailDoesNotExist");
            }
            else if (!VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("PasswordNotValid");
            }

            string token = CreateToken(user);
            response.Data = token;

            return response;
        }

        public async Task<ServiceResponse<List<WorketDTO>>> GetWorkers()
        {
            ServiceResponse<List<WorketDTO>> response = new ServiceResponse<List<WorketDTO>>();

            List<WorketDTO> workers = new List<WorketDTO>();
            workers = _databaseContext.Workers.Select(x => new WorketDTO()
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                WorkerTypeId = x.WorkerTypeId
            }).ToList();
            Console.WriteLine(workers[0].Id);
            response.Data = workers;

            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Worker user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("phoneNumber",user.PhoneNumber),
                new Claim("email",user.Email),
                new Claim("name",user.Name),
                new Claim("lastName",user.LastName),
                new Claim("workerTypeId",user.WorkerTypeId.ToString()),
                new Claim("id",user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("SECRET_KEY")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_options.Value.JWTduration),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public static string? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")!);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var Id = jwtToken.Claims.First(x => x.Type == "id").Value;

                return Id;
            }
            catch
            {
                return null;
            }

        }

        public async Task<ServiceResponse<WorkerCommunication>> GetChat(int userId, int secondUserId)
        {
            ServiceResponse<WorkerCommunication> response = new ServiceResponse<WorkerCommunication>();

            WorkerCommunication? chat = _databaseContext.WorkerCommunications.Where(x => x.User1 == userId && x.User2 == secondUserId ||
                                                                            x.User1 == secondUserId && x.User2 == userId).FirstOrDefault();
            response.Data = chat;

            return response;
        }

        public async Task<ServiceResponse<List<MessageDTO>>> GetChatMessages(int chatId)
        {
            ServiceResponse<List<MessageDTO>> response = new ServiceResponse<List<MessageDTO>>();
            response.Data = _databaseContext.WorkerMessages.Select(x => new MessageDTO
            {
                CommunicationId = x.CommunicationId,
                Id = x.Id,
                SenderId = x.SenderId,
                Message = x.Message
            }).Where(x => x.CommunicationId == chatId).ToList();
            return response;
        }

        public async Task<ServiceResponse<WorkerCommunication>> CreateWorkerChat(int User1Id, int User2Id)
        {
            ServiceResponse<WorkerCommunication> response = new ServiceResponse<WorkerCommunication>();
            WorkerCommunication communication = new WorkerCommunication
            {
                UnreadMessages = 0,
                User1 = User1Id,
                User2 = User2Id
            };
            _databaseContext.WorkerCommunications.Add(communication);
            _databaseContext.SaveChanges();
            response.Data = communication;
            return response;
        }

        public async Task<ServiceResponse<int>> PostMessage(string Message, int SenderID)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            WorkerMessage workerMessage = new WorkerMessage
            {
                Message = Message,
                SenderId = SenderID
            };
            _databaseContext.WorkerMessages.Add(workerMessage);
            _databaseContext.SaveChanges();
            response.Data = workerMessage.CommunicationId;
            return response;
        }
    }
}
