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
        if (attacker.Alive && defender.Alive)
        {

            BattleActive = true;
            if (Attacker is Enemy)
            {
                Console.WriteLine($"A wild {Attacker.Name} appears!");
            }
            else if (Defender is Enemy)
            {
                Utility.PrintColor($"A wild {Defender.Name} appears!", ConsoleColor.DarkRed);
            }
            Console.ReadKey(true);
            BattleLoop();
        }
        else
        {
            if (!attacker!.Alive)
            {
                defender.Loot(attacker);
            }
            else
            {
                attacker.Loot(defender);
            }
            return;
        }
    }
    void BattleLoop()
    {
        while (BattleActive)
        {
            turns += 1;
            if (turns % 2 != 0)
            {
                Attacker.TakeTurn(Defender);
                if (!Defender.Alive) { EndBattle(Attacker,Defender); break; }
            }
            else
            {
                Defender.TakeTurn(Attacker);
                if (!Attacker.Alive){ EndBattle(Defender,Attacker);  break;}
            }
        }
    }
    Menu EndBattle(Entity winner, Entity killed)
    {
        Console.ReadKey(true);
        if (winner is Player)
        {
            int selectedIndex = 0;
            bool subRunning = true;
            string[] yesNo = ["Loot", "Leave"];
            while (subRunning)
            {
                Console.Clear();
                Utility.GenerateMenu(killed.Name + "'s body is lifeless on the floor.");
                Utility.GenerateMenuActions(selectedIndex, yesNo);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = yesNo.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex++;
                        if (selectedIndex > yesNo.Length - 1)
                            selectedIndex = 0;
                        break;
                    case ConsoleKey.Enter:
                        if (yesNo[selectedIndex] == "Loot")
                        {
                            subRunning = false;
                            winner.Loot(killed);
                        }
                        else if (yesNo[selectedIndex] == "Leave")
                        { subRunning = false; }
                        break;
                }
            }
            return Menu.Main;
        }
        else
        {
            return Menu.GameOver;
        }
    }
}