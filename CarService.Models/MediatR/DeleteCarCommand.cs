using CarService.Models.Models;
using CarService.Models.Responses;
using MediatR;

namespace CarService.Models.MediatR
{
    public record DeleteCarCommand(int carId) : IRequest<Car>
    {
    }
}
