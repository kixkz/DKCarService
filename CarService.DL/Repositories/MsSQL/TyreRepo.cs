using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Repositories.MsSQL
{
    public class TyreRepo : ITyreRepo
    {
        private readonly ILogger<TyreRepo> _logger;
        private readonly IConfiguration _configuration;

        public TyreRepo(ILogger<TyreRepo> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Tyre> AddTyre(Tyre tyre)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Tyre] (TyreName, Price, Quantity) VALUES (@TyreName, @Price, @Quantity)", tyre);

                    return tyre;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddTyre)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Tyre> DeleteTyre(int tyreId)
        {
            var tyreForDelete = await GetTyreById(tyreId);

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("DELETE FROM Tyre WHERE Id=@id", new { id = tyreId });

                    return tyreForDelete;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteTyre)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<IEnumerable<Tyre>> GetAllTyres()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Tyre>("SELECT * FROM Tyre WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllTyres)}:{e.Message}", e);
            }

            return Enumerable.Empty<Tyre>();
        }

        public async Task<Tyre?> GetTyreById(int tyreId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Tyre>("SELECT * FROM Tyre WITH(NOLOCK) WHERE Id=@Id", new { id = tyreId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetTyreById)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Tyre?> UpdateTyre(Tyre tyre)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    await conn.QueryFirstOrDefaultAsync("UPDATE Tyre SET TyreName=@tyreName, Price=@price, Quantity=@quantity WHERE Id=@id",
                        new {id = tyre.Id, tyreName = tyre.TyreName, price = tyre.Price, quantity = tyre.Quantity });

                    return await GetTyreById(tyre.Id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateTyre)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Tyre> GetTyreByName(string tyreName)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Tyre>("SELECT * FROM Tyre WITH(NOLOCK) WHERE TyreName=@TyreName", new { TyreName = tyreName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetTyreById)}:{e.Message}", e);
            }

            return null;
        }
    }
}
