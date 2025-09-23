namespace Game;

class BattleSystem
{
    Player Player;
    Enemy Enemy;
    public int turns;
    public bool BattleStarted;
    public BattleSystem(Player player, Enemy enemy, bool battleStarted)
    {
        Player = player;
        Enemy = enemy;
        BattleStarted = battleStarted;
    }
    public void Attack(Entity Attacker, Entity Defender)
    {
        
    }
}