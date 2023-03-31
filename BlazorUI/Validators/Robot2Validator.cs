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

        /*RuleFor(r => r.Power)
            .GreaterThanOrEqualTo(-20).WithMessage("Power doit être plus grand que -20")
            .MustAsync(async (power, cancellationToken) =>
            {
                await Task.Delay(5000, cancellationToken);
                return  power % 2 == 0;
            }).WithMessage("Power doit être pair");*/
        
        RuleFor(r => r.Power)
            .GreaterThanOrEqualTo(-20).WithMessage("Power doit être plus grand que -20")
            .Must((power) =>   power % 2 == 0).WithMessage("Power doit être pair");
    }
    
}