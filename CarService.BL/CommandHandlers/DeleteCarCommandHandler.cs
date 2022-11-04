using System.Net;
using AutoMapper;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Models;
using CarService.Models.Responses;
using MediatR;

namespace CarService.BL.CommandHandlers
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Car>
    {
        private readonly ICarRepo _carRepo;

        public DeleteCarCommandHandler(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<Car> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepo.DeleteCar(request.carId);

            return result;

        }
    }
}
