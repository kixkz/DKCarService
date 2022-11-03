using AutoMapper;
using CarService.BL.Interfaces;
using CarService.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, ILogger<ClientController> logger, IMapper mapper)
        {
            _clientService = clientService;
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddClient))]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            return Ok(await _clientService.AddClient(client));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetClientById))]
        public async Task<IActionResult> GetClientById(int clientId)
        {
            if (clientId <= 0) return BadRequest($"Parameter id: {clientId} must be greater than zero");
            
            var result = await _clientService.GetById(clientId);

            if (result == null) return NotFound($"Client not exist");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteClient))]
        public async Task<IActionResult> DeleteClient( int clientId)
        {
            if (await _clientService.GetById(clientId) == null) 
            {
                return BadRequest("Client not exist");
            }

            var result = await _clientService.DeleteClient(clientId);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllClients))]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _clientService.GetAll();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateClient))]
        public async Task<IActionResult> UpdateClient(Client client, int id)
        {
            if (_clientService.GetById(id) == null)
            {
                return NotFound($"Client with this id: {id} does not exist");
            }

            return Ok(await _clientService.UpdateClient(client, id));
        }
    }
}