using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private BarberDBContext _databaseContext;
        private readonly IOptions<AppConfiguration> _options;

        public UserService(BarberDBContext userContext, IOptions<AppConfiguration> options)
        {
            _databaseContext = userContext;
            _options = options;
        }

        public async Task<ServiceResponse<bool>> RegisterUser(RegistrationDTO registrationDTO)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

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

            _databaseContext.Users.Add(user);

            _databaseContext.SaveChanges();
            response.Data = true;

  
            return response;
        }

        public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();


            var user = _databaseContext.Users.Where(x => x.Email.Equals(loginDTO.Email)).FirstOrDefault();
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("phoneNumber",user.PhoneNumber),
                new Claim("email",user.Email),
                new Claim("name",user.Name),
                new Claim("id",user.Name)
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


        private bool CheckIfUserExists(User newUser)
        {

            try
            {
                User? oldUser = _databaseContext.Users.Where(user =>
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
