using CarService.Models.Models;
using FluentValidation;

namespace CarService.Validators
{
    public class AddPurchaseValidator : AbstractValidator<Purchase>
    {
        public AddPurchaseValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.TotalMoney).NotEmpty().GreaterThan(0);
            RuleFor(x => x.TyreId).NotEmpty().GreaterThan(0);
        }
    }
}
