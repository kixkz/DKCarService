using CarService.Models.Models;

namespace CarService.DL.Interfaces
{
    public interface ICarRepo
    {
        public Task<Car> AddCar(Car car);

        public Task<Car?> GetCarById(int carId);

        public Task<IEnumerable<Car>> GetAllCars();

        public Task<Car?> UpdateCar(Car car);

        public Task<Car> DeleteCar(int carId);
    }
}
