using Domain;
using FluentValidation;

namespace BlazorUI.Validators;

public class RobotValidator : AbstractValidator<Robot>
{
    public RobotValidator()
    {
        RuleFor(r => r.CodeName)
            .NotEmpty().WithMessage("CodeName ne peut pas être vide")
            .MinimumLength(3).WithMessage("CodeName doit avoir au moins 3 caractères")
            .MaximumLength(8).WithMessage("CodeName ne peut dépasser 8 caractères");
    }
}