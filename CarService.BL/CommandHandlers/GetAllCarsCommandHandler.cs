using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Models;
using MediatR;

namespace CarService.BL.CommandHandlers
{
    public class GetAllCarsCommandHandler : IRequestHandler<GetAllCarsCommand, IEnumerable<Car>>
    {
        private readonly ICarRepo _carRepo;

        public GetAllCarsCommandHandler(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCarsCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepo.GetAllCars();

            return result;
        }
    }
}
