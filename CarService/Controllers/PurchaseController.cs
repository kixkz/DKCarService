using CarService.BL.Interfaces;
using CarService.BL.Kafka;
using CarService.BL.Services;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IClientService _clientService;
        private readonly ITyreService _tyreService;
        private readonly ILogger<PurchaseController> _logger;
        private readonly Producer<Guid, Purchase> _producer;

        public PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger, Producer<Guid, Purchase> producerService, IClientService clientService, ITyreService tyreService)
        {
            _purchaseService = purchaseService;
            _logger = logger;
            _producer = producerService;
            _clientService = clientService;
            _tyreService = tyreService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddPurchase))]
        public async Task<IActionResult> AddPurchase([FromBody] Purchase purchase)
        {
            var client = await _clientService.GetById(purchase.ClientId);
            var tyre = await _tyreService.GetTyreById(purchase.TyreId);

            if (client == null) return BadRequest("Client does not exist");
            if (tyre == null) return BadRequest("Tyre does not exist");

            if (tyre.Quantity < purchase.Quantity) return BadRequest("Not enough quantity");
            
            var result = await _purchaseService.AddPurchase(purchase);

            if (result != null)
            {
                await _producer.Produce(result.PurchaseId, result);
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetPurchaseById))]
        public async Task<IActionResult> GetPurchaseById(Guid purchaseId)
        {
            if (purchaseId == Guid.Empty) return BadRequest($"Parameter id: {purchaseId} must be greater than zero");

            var result = await _purchaseService.GetPurchaseById(purchaseId);

            if (result == null) return NotFound($"Purchase not exist");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeletePurchase))]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            if (await _purchaseService.GetPurchaseById(purchaseId) == null)
            {
                return BadRequest("Purchase not exist");
            }

            var result = await _purchaseService.DeletePurchase(purchaseId);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllPurchases))]
        public async Task<IActionResult> GetAllPurchases()
        {
            var result = await _purchaseService.GetAllPurchases();

            if (result.Count() <= 0)
            {
                return BadRequest("There aren't any purchases in the collection");
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdatePurchase))]
        public async Task<IActionResult> UpdatePurchase(Purchase purchase, Guid id)
        {
            if (_purchaseService.GetPurchaseById(id) == null)
            {
                return NotFound($"Purchase with id:{purchase.PurchaseId} does not exist");
            }

            return Ok(await _purchaseService.UpdatePurchase(purchase, id));
        }
    }
}
