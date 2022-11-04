using CarService.Models.Models;
using MediatR;

namespace CarService.Models.MediatR
{
    public record GetCarByIdCommand(int carId) : IRequest<Car>
    {
    }
}
