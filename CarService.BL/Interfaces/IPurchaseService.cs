using CarService.Models.Models;

namespace CarService.BL.Interfaces
{
    public interface IPurchaseService
    {
        public Task<Purchase> AddPurchase(Purchase purchase);

        public Task<Purchase> GetPurchaseById(Guid purchaseId);

        public Task<IEnumerable<Purchase>> GetAllPurchases();

        public Task<Purchase> UpdatePurchase(Purchase purchase, Guid id);

        public Task<Purchase> DeletePurchase(Guid purchaseId);
    }
}
