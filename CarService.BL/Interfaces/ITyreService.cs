using CarService.Models.Models;

namespace CarService.BL.Interfaces
{
    public interface ITyreService
    {
        public Task<Tyre> AddTyre(Tyre tyre);

        public Task<Tyre?> GetTyreById(int tyreId);

        public Task<IEnumerable<Tyre>> GetAllTyres();

        public Task<Tyre?> UpdateTyre(Tyre tyre, int id);

        public Task<Tyre> DeleteTyre(int tyreId);

        public Task<Tyre> GetTyreByName(string tyreName);
    }
}
