using Blazored.FluentValidation;
using BlazorUI.Services;
using Domain;
using Microsoft.AspNetCore.Components;
// ReSharper disable ClassNeverInstantiated.Global

namespace BlazorUI.Pages;

public partial class RobotForm
{
    [Inject] private IRobotRepository? Repo { get; set;  }

    private Robot Robot { get; set; } = new() { CodeName = "Mick-22"};

    private IEnumerable<Weapon> _weapons = default!;
    private FluentValidationValidator? _fluentValidationValidator;

    private bool _isSuccess = false;

    protected override async Task OnInitializedAsync()
    {
        _weapons = await Repo!.GetAllWeapons();
    }

    private async Task SubmitFormAsync()
    {
        if (await _fluentValidationValidator!.ValidateAsync())
        {
            Console.WriteLine("It's valid");
            Console.WriteLine(Robot?.FavouriteWeapon?.Name);
            _isSuccess = true;
        }
        else
        {
            Console.WriteLine("It isn't valid");
            _isSuccess = false;
        }
    }
}