using FluentValidation;

namespace BlazorUI.Validators;

public class FluentValueValidator<T> : AbstractValidator<T>
{
    public FluentValueValidator(Action<IRuleBuilder<T,T>> rule)
    {
        rule(RuleFor(x => x));
    }

    private IEnumerable<string> ValidateValue(T arg)
    {
        var result = Validate(arg);

        if (result.IsValid) return new string[0]; // Array<string>.Empty

        return result.Errors.Select((er => er.ErrorMessage));
    }

    public Func<T, IEnumerable<string>> validation => ValidateValue;
}