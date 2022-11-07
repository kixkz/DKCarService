using CarService.Models.Models;
using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Validators
{
    public class AddCarValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.CarModel).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
