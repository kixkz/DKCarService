using AutoMapper;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Requests;
using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepo _carRepo;
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarController(ICarRepo carRepo, ILogger<CarController> logger, IMediator mediator, IMapper mapper)
        {
            _carRepo = carRepo;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] AddCarRequest car)
        {
            var result = await _mediator.Send(new AddCarCommand(car));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
