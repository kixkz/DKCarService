using AutoMapper;
using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Microsoft.Extensions.Logging;

namespace CarService.BL.Services
{
    public class TyreService : ITyreService
    {
        private readonly ITyreRepo _tyreRepo;
        private readonly ILogger<TyreService> _logger;
        private readonly IMapper _mapper;

        public TyreService(ITyreRepo tyreRepo, ILogger<TyreService> logger, IMapper mapper)
        {
            _tyreRepo = tyreRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Tyre> AddTyre(Tyre tyre)
        {
            return await _tyreRepo.AddTyre(tyre);
        }

        public async Task<Tyre> DeleteTyre(int tyreId)
        {
            return await _tyreRepo.DeleteTyre(tyreId);
        }

        public async Task<IEnumerable<Tyre>> GetAllTyres()
        {
            return await _tyreRepo.GetAllTyres();
        }

        public async Task<Tyre?> GetTyreById(int tyreId)
        {
            return await _tyreRepo.GetTyreById(tyreId);
        }

        public async Task<Tyre> GetTyreByName(string tyreName)
        {
            return await _tyreRepo.GetTyreByName(tyreName);
        }

        public async Task<Tyre?> UpdateTyre(Tyre tyre, int id)
        {
            return await _tyreRepo.UpdateTyre(tyre);
        }
    }
}
