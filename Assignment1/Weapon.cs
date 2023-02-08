namespace Assignment1
{
    public class Weapon : Item
    {
        public int WeaponDamage { get; set; } 
        public WeaponTypes WeaponType { get; set; }
    }

    public enum WeaponTypes
    {
        Axes,
        Bows,
        Daggers,
        Hammers,
        Staffs,
        Swords,
        Wands
    }
}