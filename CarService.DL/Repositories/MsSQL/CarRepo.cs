using System.Data.SqlClient;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarService.DL.Repositories.MsSQL
{
    public class CarRepo : ICarRepo
    {
        private readonly ILogger<CarRepo> _logger;
        private readonly IConfiguration _configuration;

        public CarRepo(ILogger<CarRepo> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Car> AddCar(Car car)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Car] (CarName, CarModel) VALUES (@Carname, @CarModel)", car);

                    return car;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddCar)}:{e.Message}", e);
            }

            return null;
        }

        public Task<Car> DeleteCar(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public Task<Car> GetCarById(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Car> UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
