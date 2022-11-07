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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddTyre))]
        public async Task<IActionResult> AddTyre([FromBody] Tyre tyre)
        {
            return Ok(await _tyreService.AddTyre(tyre));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllTyres))]
        public async Task<IActionResult> GetAllTyres()
        {
            var result = await _tyreService.GetAllTyres();

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

            if (result == null) return NotFound($"Tyre not exist");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateTyre))]
        public async Task<IActionResult> UpdateTyre(Tyre tyre)
        {
            return Ok(await _tyreService.UpdateTyre(tyre));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteTyre))]
        public async Task<IActionResult> DeleteTyre(int tyreId)
        {
            if (await _tyreService.GetTyreById(tyreId) == null)
            {
                return BadRequest("Client not exist");
            }

            var result = await _tyreService.DeleteTyre(tyreId);

            return Ok(result);
        }
    }
}
