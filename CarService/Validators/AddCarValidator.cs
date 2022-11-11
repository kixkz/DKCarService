using CarService.Models.Models;
using CarService.Models.Requests;
using FluentValidation;

namespace CarService.Validators
{
    public class AddCarValidator : AbstractValidator<AddCarRequest>
    {
        public AddCarValidator()
        {
            RuleFor(x => x.CarName).NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50)
                .Matches(@"^[A-Z][A-Za-z]+$")
                .WithMessage("Тhe name must start with a capital letter and contain only letters");
            RuleFor(x => x.CarModel).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
