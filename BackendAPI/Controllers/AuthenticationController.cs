using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using BackendAPI.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly AppConfiguration _appConfiguration;
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IOptions<AppConfiguration> options,IUserService userService)
        {
            _logger = logger;
            _appConfiguration = options.Value;
            _userService = userService;
        }





        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ServiceResponse<bool>>> Register(RegistrationDTO dto)
        {
            ServiceResponse<bool> response=new ServiceResponse<bool>();
            try
            {
                response=await _userService.RegisterUser(dto);

            }
            catch (Exception ex) {
                response.Success = false;
                response.Message= ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDTO dto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                response = await _userService.Login(dto);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
