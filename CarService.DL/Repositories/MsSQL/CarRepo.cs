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

        public async Task<Car> DeleteCar(int carId)
        {
            var carForDelete = await GetCarById(carId);

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("DELETE FROM Car WHERE Id=@id", new { id = carId });

                    return carForDelete;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteCar)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Car>("SELECT * FROM Car WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllCars)}:{e.Message}", e);
            }

            return Enumerable.Empty<Car>();
        }

        public async Task<Car?> GetCarById(int carId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Car>("SELECT * FROM Car WITH(NOLOCK) WHERE Id=@Id", new { id = carId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarById)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Car?> UpdateCar(Car car)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    await conn.QueryFirstOrDefaultAsync("UPDATE Car SET CarName=@carName, CarModel=@carModel WHERE Id=@id",
                        new { id = car.Id, carName = car.CarName, carModel = car.CarModel});

                    return await GetCarById(car.Id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateCar)}:{e.Message}", e);
            }

            return null;
        }
    }
}
