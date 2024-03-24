using BackendAPI.Data;
using BackendAPI.Models.DTO;
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
        private readonly UserContext _userContext;

        public AuthenticationController(ILogger<AuthenticationController> logger, IOptions<AppConfiguration> options, UserContext userContext)
        {
            _logger = logger;
            _appConfiguration = options.Value;
            _userContext = userContext;
        }





        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RegistrationDTO>> Register(RegistrationDTO dto)
        {
            try
            {
                _userContext.RegisterUser(dto);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
            return Ok(dto);
        }
    }
}
