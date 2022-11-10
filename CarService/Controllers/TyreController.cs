using System.Threading;
using CarService.BL.Interfaces;
using CarService.BL.Services;
using CarService.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TyreController : ControllerBase
    {
        private readonly ITyreService _tyreService;
        private readonly ILogger<TyreController> _logger;


        public TyreController(ITyreService tyreService, ILogger<TyreController> logger)
        {
            _tyreService = tyreService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddTyre))]
        public async Task<IActionResult> AddTyre([FromBody] Tyre tyre)
        {
            var result = await _tyreService.GetTyreByName(tyre.TyreName);

            if (result != null)
            {
                return NotFound("Tyre already exist");
            }

            return Ok(await _tyreService.AddTyre(tyre));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllTyres))]
        public async Task<IActionResult> GetAllTyres()
        {
            var result = await _tyreService.GetAllTyres();

            if (result.Count() <= 0)
            {
                return BadRequest("There aren't any tyres in the collection");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetTyreById))]
        public async Task<IActionResult> GetTyreById(int tyreId)
        {
            if (tyreId <= 0) return BadRequest($"Parameter id: {tyreId} must be greater than zero");

            var result = await _tyreService.GetTyreById(tyreId);

            if (result == null) return NotFound($"Tyre does not exist");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateTyre))]
        public async Task<IActionResult> UpdateTyre(Tyre tyre, int id)
        {
            if (id <= 0) return BadRequest($"Parameter id: {id} must be greater than zero");

            var tyreToUpdate = await _tyreService.GetTyreById(id);

            if (tyreToUpdate == null) return NotFound($"Tyre with id:{id} does not exist");

            return Ok(await _tyreService.UpdateTyre(tyre, id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(nameof(DeleteTyre))]
        public async Task<IActionResult> DeleteTyre(int tyreId)
        {
            if (tyreId <= 0) return BadRequest($"Parameter id: {tyreId} must be greater than zero");

            if (await _tyreService.GetTyreById(tyreId) == null)
            {
                return NotFound($"Tyre does not exist");
            }

            var result = await _tyreService.DeleteTyre(tyreId);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetTyreByName))]
        public async Task<IActionResult> GetTyreByName(string tyreName)
        {
            var result = await _tyreService.GetTyreByName(tyreName);

            if (result == null)
            {
                return NotFound($"Tyre does not exist");
            }

            return Ok(result);
        }
    }
}
