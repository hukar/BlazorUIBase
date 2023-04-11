using Domain;
using FluentValidation;

namespace BlazorUI.Validators;

public class Robot2Validator : AbstractValidator<Robot2>
{
    public Robot2Validator()
    {
        RuleFor(r => r.CodeName)
            .NotNull()
            .MinimumLength(3).WithMessage("3 lettres minimum")
            .MaximumLength(8).WithMessage("8 lettres maximum");

        RuleFor(r => r.Power)
            .GreaterThanOrEqualTo(-20).WithMessage("Power doit être plus grand que -20")
            .MustAsync(IsOddAsync).WithMessage("Power doit être pair");
    }
    
    private async Task<bool> IsOddAsync(int power, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        return  power % 2 == 0;
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<Robot2>.CreateWithOptions((Robot2)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
    
}