using CarService.Models.Models;
using FluentValidation;

namespace CarService.Validators
{
    public class AddTyreValidator : AbstractValidator<Tyre>
    {
        public AddTyreValidator()
        {
            RuleFor(x => x.TyreName).NotEmpty()
                .MinimumLength(2).MaximumLength(50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("Тhe name must start with a capital letter and contain only letters");
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
        }
    }
}
