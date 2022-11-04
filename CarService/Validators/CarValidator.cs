using CarService.Models.Models;
using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Validators
{
    public class CarValidator : AbstractValidator<AddCarRequest>
    {
        public CarValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.CarModel).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
