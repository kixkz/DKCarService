using System.Data.SqlClient;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Repositories.MsSQL
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly ILogger<PurchaseRepo> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITyreRepo _tyreRepo;

        public PurchaseRepo(ILogger<PurchaseRepo> logger, IConfiguration configuration, ITyreRepo tyreRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _tyreRepo = tyreRepo;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Purchase] (ClientId, TyreId, Quantity, TotalMoney) VALUES (@ClientId, @TyreId, @Quantity, @TotalMoney)", purchase);

                    return purchase;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddPurchase)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Purchase> DeletePurchase(Guid purchaseId)
        {
            var purchaseForDelete = await GetPurchaseById(purchaseId);

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("DELETE FROM Purchase WHERE PurchaseId=@PurchaseId", new { PurchaseId = purchaseId });

                    return purchaseForDelete;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeletePurchase)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Purchase>("SELECT * FROM Purchase WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllPurchases)}:{e.Message}", e);
            }

            return Enumerable.Empty<Purchase>();
        }

        public async Task<Purchase> GetPurchaseById(Guid purchaseId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Purchase>("SELECT * FROM Purchase WITH(NOLOCK) WHERE PurchaseId=@PurchaseId", new { PurchaseId = purchaseId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetPurchaseById)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Purchase> UpdatePurchase(Purchase purchase)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    await conn.QueryFirstOrDefaultAsync("UPDATE Purchase SET ClientId=@clientId, TyreId=@tyreId, Quantity=@quantity, TotalMoney=@totalMoney WHERE PurchaseId=@purchaseId",
                        new { purchaseId = purchase.PurchaseId, clientId = purchase.ClientId, TyreId = purchase.TyreId, quantity = purchase.Quantity, totalMoney = purchase.TotalMoney });

                    return await GetPurchaseById(purchase.PurchaseId);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdatePurchase)}:{e.Message}", e);
            }

            return null;
        }
    }
}
