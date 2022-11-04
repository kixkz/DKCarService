using CarService.Models.Models;
using FluentValidation;

namespace CarService.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.ClientName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.City).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Age).NotNull().NotEmpty().GreaterThan(18).LessThan(80);
            RuleFor(x => x.CarId).NotEmpty().GreaterThan(0);
        }
    }
}
