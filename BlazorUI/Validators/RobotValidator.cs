using BlazorUI.Services;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Validators;

public class RobotValidator : AbstractValidator<Robot>
{
    [Inject] private IRobotRepository? Repo { get; set;  }

    public RobotValidator()
    {
        RuleFor(r => r.CodeName)
            .NotEmpty().WithMessage("CodeName ne peut pas être vide")
            .MinimumLength(3).WithMessage("CodeName doit avoir au moins 3 caractères")
            .MaximumLength(8).WithMessage("CodeName ne peut dépasser 8 caractères");

        RuleFor(r => r.FavouriteWeapon)
            .NotEmpty().WithMessage("Une arme est obligatoire")
            .MustAsync(IsWeaponExist).WithMessage("cette weapon n'existe pas");
        
        RuleForEach(r => r.Weapons)
            .MustAsync(IsWeaponExist).WithMessage("cette weapon n'existe pas");
    }

    private async Task<bool> IsWeaponExist(Weapon? weapon, CancellationToken _)
        => weapon is null ? await Task.FromResult(false) : await Repo!.IsWeaponExists(weapon);
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<Robot>.CreateWithOptions((Robot)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid) return Array.Empty<string>();
        
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

public class WeaponValidator : AbstractValidator<Weapon>
{
    [Inject] private IRobotRepository? Repo { get; set;  }
        
    public WeaponValidator()
    {
        RuleFor(w => w)
            .NotEmpty().WithMessage("Une arme est obligatoire")
            .MustAsync(IsWeaponExist).WithMessage("cette weapon n'existe pas");
    }
        
    private async Task<bool> IsWeaponExist(Weapon? weapon, CancellationToken _)
        => weapon is null ? await Task.FromResult(false) : await Repo!.IsWeaponExists(weapon);
}

