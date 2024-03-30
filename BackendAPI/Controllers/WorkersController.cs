using BackendAPI.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly ILogger<WorkersController> _logger;
        private readonly AppConfiguration _appConfiguration;
        private readonly IUserService _userService;

        public WorkersController(ILogger<WorkersController> logger, IOptions<AppConfiguration> options, IUserService userService)
        {
            _logger = logger;
            _appConfiguration = options.Value;
            _userService = userService;
        }
        // GET: api/<WorkersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WorkersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorkersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WorkersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
