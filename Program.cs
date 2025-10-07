using System.Xml.Serialization;
using Game;

Item longsword = new Item("Longsword", "weapon");
longsword.DefineItem(2);
Item healthPotion = new Item("Health Potion", "consumable");
healthPotion.DefineItem(3);

Player player1 = new Player("Max", 20, 10, 2, 100, 1, 5, 20);
player1.AddItem(longsword);
player1.AddItem(healthPotion);

Enemy goblin1 = new Enemy("Goblin Soldier", 5, 5, 2, 100, 1, 3, "Goblin", 5);

bool running = true;
while (running)
{
    string choice = "";
    Console.Clear();
    Utility.GenerateMenu(title: "Choose an option: ", choices: new[] { "Attack enemy", "Character", "Leave" });
    int.TryParse(Console.ReadLine(), out int input);
    switch (input)
    {
        case 1:
            BattleSystem battle = new BattleSystem(player1, goblin1);
            break;
        case 2:
            Console.Clear();
            Utility.GenerateMenu(title: "Choose an option: ", choices: new[] { "Equip Item", "Unequip item","Inventory", "Equipped","Check stats" });
            int.TryParse(Console.ReadLine(), out input);
            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(player1.CheckInventory());
                    int.TryParse(Console.ReadLine(),out input);
                    player1.EquipItem(player1.Inventory[input-1]);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine(player1.CheckEquipped());
                    int.TryParse(Console.ReadLine(),out input);
                    player1.UnEquipItem(player1.Equipped[input-1]);
                    break;
                case 3:
                    Console.Clear();
                    choice = Utility.Prompt(player1.CheckInventory());
                    if (string.IsNullOrWhiteSpace(choice)) { break; }
                    int.TryParse(choice, out int nr);
                    Console.WriteLine(player1.Inventory[nr - 1].Info());
                    Console.ReadLine();
                    break;
                case 4:
                    Console.Clear();
                    Utility.Prompt(player1.CheckEquipped());
                    break;
                case 5:
                    Console.Clear();
                    Utility.Prompt(player1.Info());
                    break;
                default:
                    Utility.Error("Something went wrong in sub-menu input");
                    break;
            }
            break;
        case 3:
            choice = Utility.Prompt("Are you sure?");
            if (string.IsNullOrWhiteSpace(choice)) { break; }

            break;
        default:
            Utility.Error("Something went wrong in menu input");
            break;
    }
}