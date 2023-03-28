namespace Domain;

public class Robot
{
    public int Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
    public IEnumerable<Weapon> Weapons { get; set; }  = new List<Weapon>();

    public Weapon? FavouriteWeapon { get; set; }
}