using Domain;

namespace BlazorUI.Services;

public interface IRobotRepository
{
    Task<IEnumerable<Robot>> GetAllRobots();
    Task<Robot?> GetRobotWithWeapons(int id);
    Task AddRobot(Robot robotToAdd);
}