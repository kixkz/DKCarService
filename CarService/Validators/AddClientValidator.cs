using CarService.Models.Models;
using FluentValidation;

namespace CarService.Validators
{
    public class AddClientValidator : AbstractValidator<Client>
    {
        public AddClientValidator()
        {
            RuleFor(x => x.ClientName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("Тhe name must start with a capital letter and contain only letters");
            RuleFor(x => x.City).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("Тhe name must start with a capital letter and contain only letters");
            RuleFor(x => x.Age).NotNull().NotEmpty().GreaterThan(18).LessThan(80);
            RuleFor(x => x.CarId).NotEmpty().GreaterThan(0);
        }
    }
}
