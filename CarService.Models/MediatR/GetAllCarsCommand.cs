using CarService.Models.Models;
using MediatR;

namespace CarService.Models.MediatR
{
    public record GetAllCarsCommand : IRequest<IEnumerable<Car>>
    {
    }
}
