using AutoMapper;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Responses;
using System.Net;
using MediatR;
using CarService.Models.Models;

namespace CarService.BL.CommandHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, AddCarResponse>
    {
        private readonly ICarRepo _carRepo;
        private IMapper _mapper;

        public UpdateCarCommandHandler(ICarRepo carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;
        }

        public async Task<AddCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var auth = await _carRepo.GetCarById(request.car.Id);

            if (auth == null)
            {
                return new AddCarResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Car not exist"
                };
            }

            var car = _mapper.Map<Car>(request.car);
            var result = _carRepo.UpdateCar(car);
            return new AddCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Car = await result
            };
        }
    }
}
