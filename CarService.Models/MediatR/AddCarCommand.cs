using CarService.Models.Requests;
using CarService.Models.Responses;
using MediatR;

namespace CarService.Models.MediatR
{
    public record AddCarCommand(AddCarRequest car) : IRequest<AddCarResponse>
    {
    }
}
