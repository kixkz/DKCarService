using CarService.Models.Models;

namespace CarService.DL.Interfaces
{
    public interface IPurchaseRepo
    {
        public Task<Purchase> AddPurchase(Purchase purchase);

        public Task<Purchase> GetPurchaseById(Guid purchaseId);

        public Task<IEnumerable<Purchase>> GetAllPurchases();

        public Task<Purchase> UpdatePurchase(Purchase purchase);

        public Task<Purchase> DeletePurchase(Guid purchaseId);
    }
}
