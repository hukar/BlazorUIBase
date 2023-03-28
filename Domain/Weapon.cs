namespace Domain;

public class Weapon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RobotId { get; set; }
    
    // Note: this is important so the MudSelect can compare Weapons
    public override bool Equals(object? o)
    {
        var other = o as Weapon;
        return other?.Name == Name;
    }

    // Note: this is important too!
    public override int GetHashCode() => Name?.GetHashCode() ?? 0;
    
    public override string ToString() => Name;
}