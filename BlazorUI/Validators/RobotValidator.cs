using BlazorUI.Services;
using Domain;
using FluentValidation;

namespace BlazorUI.Validators;

public class RobotValidator : AbstractValidator<Robot>
{
    private readonly IRobotRepository _repo;

    public RobotValidator(IRobotRepository repo)
    {
        _repo = repo;
        
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

    private async Task<bool> IsWeaponExist(Robot robot,
        Weapon? weapon, ValidationContext<Robot> context, CancellationToken token)
        => weapon is null ? await Task.FromResult(false) : await _repo.IsWeaponExists(weapon);
}

