namespace Game;

class Weapon : Item
{
    double CritChance = 0.05;
    double CritDamage = 1.20;
    WeaponType Type;
    public Weapon(string name, int damage, WeaponType type)
    : base(name, damage)
    {
        Type = type;
        switch (type)
        {
            case WeaponType.Axe:
                damage *= Convert.ToInt32(Math.Round(1.3));
                CritChance += 0.2;
                CritDamage += 0.3;
                break;
            case WeaponType.Sword:
                damage *= Convert.ToInt32(Math.Round(1.15));
                CritChance += 0.05;
                CritDamage += 0.1;
                break;
            case WeaponType.Dagger:
                damage *= Convert.ToInt32(Math.Round(0.90));
                CritChance += 0.3;
                CritDamage += 0.5;
                break;
            case WeaponType.Mace:
                damage *= Convert.ToInt32(Math.Round(1.4));
                CritChance += 0;
                CritDamage += 1;
                break;
        }
    }
}
enum WeaponType
{
    Axe,
    Sword,
    Dagger,
    Mace,
}