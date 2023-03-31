using FluentValidation;

namespace BlazorUI.Validators;

public class CreditCardValidator : AbstractValidator<string>
{
    public CreditCardValidator()
    {
        RuleFor(cc => cc)
            .NotEmpty().WithMessage("Ne soit pas null Credit Card ni vide")
            .Length(1, 100)
            .CreditCard().WithMessage("Numero de carte invalide");
    }
}