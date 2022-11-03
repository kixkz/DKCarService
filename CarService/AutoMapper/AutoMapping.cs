using AutoMapper;
using CarService.Models.Models;
using CarService.Models.Requests;

namespace CarService.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddCarRequest, Car>();
        }
    }
}
