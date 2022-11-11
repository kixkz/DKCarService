using System.Net;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepo _carRepo;
        private readonly IMediator _mediator;

        public CarController(ICarRepo carRepo, IMediator mediator)
        {
            _carRepo = carRepo;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddCar))]
        public async Task<IActionResult> AddCar([FromBody] AddCarRequest car)
        {
            var result = await _mediator.Send(new AddCarCommand(car));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteCar))]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0) return BadRequest("Parameter id must be greater than zero");

            var result = await _mediator.Send(new DeleteCarCommand(id));

            if (result == null)
            {
                return NotFound("Car does not exist");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete(nameof(GetAllCars))]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _mediator.Send(new GetAllCarsCommand());

            if (cars.Count() <= 0)
            {
                return NotFound("There aren't any cars in the collection");
            }

            return Ok(cars);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut(nameof(UpdateCar))]
        public async Task<IActionResult> UpdateCar(UpdateCarRequest car)
        {
            if (car == null) return BadRequest("Car can not be null");

            var result = await _mediator.Send(new UpdateCarCommand(car));

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetCarById))]
        public async Task<IActionResult> GetCarById(int id)
        {
            if (id <= 0) return BadRequest($"Parameter id:{id} must be greater than zero");

            var result = await _mediator.Send(new GetCarByIdCommand(id));

            if (result == null) return NotFound($"Car with id:{id} does not exist");

            return Ok(result);
        }
    }
}
