using System.Security.Cryptography.X509Certificates;
using System.Threading;
using AutoMapper;
using CarService.BL.Interfaces;
using CarService.BL.Services;
using CarService.DL.Interfaces;
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
        private readonly ICarRepo _carRepo;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, ILogger<ClientController> logger, IMapper mapper, ITyreService treyService, ICarRepo carRepo)
        {
            _clientService = clientService;
            _logger = logger;
            _mapper = mapper;
            _carRepo = carRepo;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost(nameof(AddClient))]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            var result = await _clientService.GetClientByName(client.ClientName);

            if (result != null)
            {
                return NotFound("Client already exist");
            }

            var carId = await _carRepo.GetCarById(client.CarId);

            if (carId == null)
            {
                return BadRequest("Car does not exist");
            }

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

            if (result == null) return NotFound($"Client with id:{clientId} does not exist");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(nameof(DeleteClient))]
        public async Task<IActionResult> DeleteClient( int clientId)
        {
            if (clientId <= 0) return BadRequest($"Parameter id: {clientId} must be greater than zero");

            if (await _clientService.GetById(clientId) == null) 
            {
                return NotFound($"Client with id:{clientId} does not exist");
            }

            var result = await _clientService.DeleteClient(clientId);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllClients))]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _clientService.GetAll();

            if (result.Count() <= 0)
            {
                return BadRequest("There aren't any clients in the collection");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateClient))]
        public async Task<IActionResult> UpdateClient(Client client, int id)
        {
            if (id <= 0) return BadRequest($"Parameter id: {id} must be greater than zero");

            if (await _clientService.GetById(id) == null)
            {
                return NotFound($"Client with id: {id} does not exist");
            }

            return Ok(await _clientService.UpdateClient(client, id));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetClientByName))]
        public async Task<IActionResult> GetClientByName(string clientName)
        {
            if (!clientName.All(Char.IsLetter))
            {
                return BadRequest("The name must contains only letters");
            }

            var result = await _clientService.GetClientByName(clientName);

            if (result == null)
            {
                return NotFound($"Client with name:{clientName} does not exist");
            }

            return Ok(result);
        }
    }
}