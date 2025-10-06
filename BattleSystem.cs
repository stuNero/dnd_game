namespace Game;

class BattleSystem
{
    Entity Attacker;
    Entity Defender;
    public int turns;
    bool BattleActive;
    public BattleSystem(Entity attacker, Entity defender)
    {
        Attacker = attacker;
        Defender = defender;

        BattleActive = true;
        if (attacker is Enemy e)
        {
            Console.WriteLine($"A wild {e.Name} appears!");
        }
        else if (defender is Enemy f)
        {
            Console.WriteLine($"A wild {f.Name} appears!");
        }
        BattleLoop();
    }
    public void BattleLoop()
    {
        while (BattleActive)
        {
            turns += 1;
            if (turns % 2 != 0)
            {
                AttackerTurn();
                if (!Defender.Alive) { EndBattle(); break; }
            }
            else
            {
                DefenderTurn();
                if (!Attacker.Alive){ EndBattle();  break;}
            }
        }
    }
    void AttackerTurn()
    {
        
    }
    void DefenderTurn()
    {

    }
    void EndBattle()
    {

    }
}