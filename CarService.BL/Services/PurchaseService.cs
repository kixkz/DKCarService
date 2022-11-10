using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Microsoft.Extensions.Logging;

namespace CarService.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepo _purchaseRepo;
        private readonly ITyreRepo _tyreRepo;
        private readonly ILogger<PurchaseService> _logger;

        public PurchaseService(IPurchaseRepo purchaseRepo, ILogger<PurchaseService> logger, ITyreRepo tyreRepo)
        {
            _purchaseRepo = purchaseRepo;
            _logger = logger;
            _tyreRepo = tyreRepo;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            var tyre = await _tyreRepo.GetTyreById(purchase.TyreId);

            tyre.Quantity -= purchase.Quantity;
            purchase.TotalMoney = purchase.Quantity * tyre.Price;

            await _tyreRepo.UpdateTyre(tyre);

            return await _purchaseRepo.AddPurchase(purchase);
        }

        public async Task<Purchase> DeletePurchase(Guid purchaseId)
        {
            return await _purchaseRepo.DeletePurchase(purchaseId);
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases()
        {
            return await _purchaseRepo.GetAllPurchases();
        }

        public async Task<Purchase> GetPurchaseById(Guid purchaseId)
        {
            return await _purchaseRepo.GetPurchaseById(purchaseId);
        }

        public async Task<Purchase> UpdatePurchase(Purchase purchase, Guid id)
        {
            //purchase.PurchaseId = id;
            return await _purchaseRepo.UpdatePurchase(purchase);
        }
    }
}
