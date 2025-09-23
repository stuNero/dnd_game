namespace Game;

class Enemy : Actor
{
    public string Type;
    public Enemy(string name, int hp, int mp, int dmg, int xp, int level, int inventorySize, string type, int maxHP)
            : base(name, hp, mp, dmg, xp, level, inventorySize, maxHP)
    {
        Type = type;
    }
}