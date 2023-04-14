using System.Net;
using System.Net.Http.Json;
using Domain;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Services;

public class RobotRepository : IRobotRepository
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public RobotRepository(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task<IEnumerable<Robot>> GetAllRobots()
    {
        var robots = await _httpClient.GetFromJsonAsync<IEnumerable<Robot>>("robots");

        return robots ?? new List<Robot>();
    }

    public async Task<Robot?> GetRobotWithWeapons(int id)
    {
        Robot? robot = null;

        try
        {
            robot = await _httpClient.GetFromJsonAsync<Robot>($"/robots/{id}/withweapons");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.StatusCode);

            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _navigationManager.NavigateTo("/404", forceLoad: true, replace:true);
            }
            else
            {
                _navigationManager.NavigateTo("/error");
            }

        }

        return robot;
    }
    

    public async Task<IEnumerable<Weapon>> GetAllWeapons()
        => await _httpClient.GetFromJsonAsync<IEnumerable<Weapon>>("/robots/weaponslist") 
           ?? new List<Weapon>();

    public Task AddRobot(Robot robotToAdd)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsWeaponExists(Weapon weapon)
        => await _httpClient.GetFromJsonAsync<bool>(
            $"/robots/isweaponexists?id={weapon.Id}&name={weapon.Name}"
        );
}