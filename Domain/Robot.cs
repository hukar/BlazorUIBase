namespace Domain;

public class Robot
{
    public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public List<Weapon> Weapons { get; set; } = new();
}