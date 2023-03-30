using Domain;

namespace BlazorUI.Services;

public interface IRobotRepository
{
    Task<IEnumerable<Robot>> GetAllRobots();
    Task<Robot?> GetRobotWithWeapons(int id);
    Task<IEnumerable<Weapon>> GetAllWeapons();
    Task AddRobot(Robot robotToAdd);
    Task<bool> IsWeaponExists(Weapon weapon);
}