using Blazored.FluentValidation;
using BlazorUI.Services;
using Domain;

namespace BlazorUI.Pages;

public partial class RobotForm
{
    private readonly IRobotRepository _repo;

    public RobotForm()
    {
        
    }


    public RobotForm(IRobotRepository Repo)
    {
        _repo = Repo;
    }

// private IEnumerable<Weapon> _options = new HashSet<Weapon>();
    private Robot Robot { get; set; } = new() { CodeName = "Mick-22"};

    private IEnumerable<Weapon> _weapons = default!;
    private FluentValidationValidator? _fluentValidationValidator;

    private bool _isSuccess = false;

    protected override async Task OnInitializedAsync()
    {
        _weapons = await _repo.GetAllWeapons();
    }

    void ValidSubmitHandle()
    {
        // Robot.Weapons.AddRange(_options.ToList());

        Console.WriteLine(Robot.CodeName);

        Console.WriteLine("Valid Submit");
        _isSuccess = true;
    }

    void InvalidSubmitHandle()
    {
        Console.WriteLine("Invalid Submit");
        _isSuccess = false;
    }

    async Task SubmitFormAsync()
    {
        if (await _fluentValidationValidator!.ValidateAsync())
        {
            Console.WriteLine("It's valid");
            Console.WriteLine(Robot.FavouriteWeapon.Name);
            _isSuccess = true;
        }
        else
        {
            Console.WriteLine("It isn't valid");
            _isSuccess = false;
        }
    }
}