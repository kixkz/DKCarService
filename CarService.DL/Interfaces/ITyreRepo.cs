using CarService.Models.Models;

namespace CarService.DL.Interfaces
{
    public interface ITyreRepo
    {
        public Task<Tyre> AddTyre(Tyre tyre);

        public Task<Tyre?> GetTyreById(int tyreId);

        public Task<IEnumerable<Tyre>> GetAllTyres();

        public Task<Tyre?> UpdateTyre(Tyre tyre);

        public Task<Tyre> DeleteTyre(int tyreId);
    }
}
