using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Game;

abstract class Actor : Entity
{
    public int MaxHP;
    public int Mp;
    public int Dmg;
    public int Xp;
    public int XpDrop;
    public int Level;

    public Actor(string name, int hp, int mp, int dmg, int xp, int level, int inventorySize, int maxHP)
            : base(name, hp, inventorySize)
    {
        MaxHP = maxHP;
        Mp = mp;
        Dmg = dmg;
        Xp = xp;
        XpDrop = Xp / 100;
        Level = level;
        Alive = true;
    }
    public string Info()
    {
        string txt = "___________________\n";

        txt += $"Name: [{Name}]\n"
             + $"LVL:  [{Level}]\n"
             + $"HP:   [{Hp}]\n"
             + $"MP:   [{Mp}]\n"
             + $"DMG:  [{Dmg}]\n";
        txt += "___________________";
        return txt;
    }
    public void UpdateStats()
    {
        foreach (Item item in Inventory)
        {

        }
    }
}