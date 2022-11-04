using CarService.Models.Models;
using MediatR;

namespace CarService.Models.MediatR
{
    public record DeleteCarCommand(int carId) : IRequest<Car>
    {
    }
}
