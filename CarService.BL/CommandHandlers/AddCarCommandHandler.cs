using AutoMapper;
using CarService.DL.Interfaces;
using CarService.Models.MediatR;
using CarService.Models.Models;
using CarService.Models.Responses;
using MediatR;
using System.Net;

namespace CarService.BL.CommandHandlers
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, AddCarResponse>
    {
        private readonly ICarRepo _carRepo;
        private readonly IMapper _mapper;

        public AddCarCommandHandler(ICarRepo carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;
        }

        public async Task<AddCarResponse> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request.car);
            var result = await _carRepo.AddCar(car);

            return new AddCarResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Car = car
            };
        }
    }
}
