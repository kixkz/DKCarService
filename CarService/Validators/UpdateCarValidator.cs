using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Validators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarRequest>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.CarModel).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
