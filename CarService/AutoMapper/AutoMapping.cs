using AutoMapper;
using CarService.Models.MediatR;
using CarService.Models.Models;
using CarService.Models.Requests;

namespace CarService.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddCarRequest, Car>();
            CreateMap<DeleteCarRequest, Car>();
            CreateMap<UpdateCarRequest, Car>();
        }
    }
}
