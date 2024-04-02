﻿using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BackendAPI.Services.WorkerService
{
    public class WorkerService:IWorkerService
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

        public async Task<ServiceResponse<List<Worker>>> GetWorkers()
        {
            ServiceResponse<List<Worker>> response = new ServiceResponse<List<Worker>>();

            List<Worker> workers = new List<Worker>();
            workers = _databaseContext.Workers.ToList();
            response.Data = workers;

            return response;
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

        private string CreateToken(Worker user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("PhoneNumber",user.PhoneNumber),
                new Claim("Email",user.Email),
                new Claim("Name",user.Name),
                new Claim("LastName",user.LastName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
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
    }
}