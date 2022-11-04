using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Models;
using MediatR;

namespace CarService.BL.CommandHandlers
{
    public class GetCarByIdCommandHandler : IRequestHandler<GetCarByIdCommand, Car>
    {
        private readonly ICarRepo _carRepo;

        public GetCarByIdCommandHandler(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<Car?> Handle(GetCarByIdCommand request, CancellationToken cancellationToken)
        {
            return await _carRepo.GetCarById(request.carId);
        }
    }
}
