using System.Net.Http.Json;
using Domain;

namespace BlazorUI.Services;

public class RobotRepository : IRobotRepository
{
    private readonly HttpClient _httpClient;

    public RobotRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Robot>> GetAllRobots()
    {
        var robots = await _httpClient.GetFromJsonAsync<IEnumerable<Robot>>("robots");

        return robots ?? new List<Robot>();
    }

    public async Task<Robot?> GetRobotWithWeapons(int id)
        => await _httpClient.GetFromJsonAsync<Robot>($"/robots:{id}/weapons");

    public Task AddRobot(Robot robotToAdd)
    {
        throw new NotImplementedException();
    }
}