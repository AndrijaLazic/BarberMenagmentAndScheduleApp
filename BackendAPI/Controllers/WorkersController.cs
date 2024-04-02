using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using BackendAPI.Models;
using BackendAPI.Services.UserService;
using BackendAPI.Services.WorkerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly ILogger<WorkersController> _logger;
        private readonly AppConfiguration _appConfiguration;
        private readonly IWorkerService _workerService;

        public WorkersController(ILogger<WorkersController> logger, IOptions<AppConfiguration> options, IWorkerService workerService)
        {
            _logger = logger;
            _appConfiguration = options.Value;
            _workerService = workerService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ServiceResponse<bool>>> Register(WorkerRegistrationDTO dto)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                response = await _workerService.RegisterWorker(dto);

            }
            catch(DbUpdateException ex)
            {
                response.Success = false;

                if (ex.InnerException != null)
                {

                    if (ex.InnerException is SqlException sqlEx)
                    {
                        response.Message= sqlEx.Message;
                        return BadRequest(response);
                    }
 
                }
                response.Message=ex.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                Console.WriteLine(ex);
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
                response = await _workerService.Login(dto);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("Workers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ServiceResponse<List<Worker>>>> GetWorkers()
        {
            ServiceResponse<List<Worker>> response = new ServiceResponse<List<Worker>>();
            try
            {
                response = await _workerService.GetWorkers();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("WorkerChat")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ServiceResponse<List<Worker>>>> GetWorkerChat(string secondUserId, [FromHeader]string JWT)
        {
            ServiceResponse<List<WorkerMessage>> response = new ServiceResponse<List<WorkerMessage>>();
            ServiceResponse<WorkerCommunication> chatResponse;
            try
            {
                chatResponse = await _workerService.GetChat(JWT, int.Parse(secondUserId));

                response = await _workerService.GetChatMessages(chatResponse.Data!.Id);

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
