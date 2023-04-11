using System.Text.Json;
using Blazored.FluentValidation;
using BlazorUI.Services;
using Domain;
using Microsoft.AspNetCore.Components;
using MudBlazor;

// ReSharper disable ClassNeverInstantiated.Global

namespace BlazorUI.Pages;

public partial class RobotForm
{
    [Inject] private IRobotRepository? Repo { get; set;  }

    private MudSelect<Weapon> _multiSelect;

    private Robot _robot = new();
    private bool _formBeingProcessed;
    private bool _formBeingSubmited;

    private IEnumerable<Weapon> _weapons = new List<Weapon>();
    private FluentValidationValidator? _fluentValidationValidator;

    private bool _isSuccess = false;

    protected override async Task OnInitializedAsync()
    {
        _weapons = await Repo!.GetAllWeapons();
    }

    private async Task SubmitFormAsync()
    {
        _formBeingProcessed = true;
        var isFormValid = await _fluentValidationValidator!.ValidateAsync();
        
        if (isFormValid)
        {
            Console.WriteLine("It's valid");
            _isSuccess = true;
            _formBeingSubmited = true;
            
            StateHasChanged();

            var robotStr = JsonSerializer.Serialize(_robot, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            Console.WriteLine(robotStr);
            _robot = new Robot();
            await Task.Delay(3000);
            
            _formBeingProcessed = false; // redirection
            _formBeingSubmited = false;
            _isSuccess = false;
        }
        else
        {
            Console.WriteLine("It isn't valid");
            _isSuccess = false;
            await Task.Delay(1000);
            _formBeingProcessed = false;
        }
    }
}