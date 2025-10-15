namespace Game;
using System.Diagnostics;
class Player : Actor
{
    public Item?[] Equipped = new Item?[3];
    public Player(string name, int maxHP, int mp, int dmg, int xp, int level, int inventorySize)
                : base(name, maxHP, mp, dmg, xp, level, inventorySize)
    { }
    public string CheckInventory()
    {
        // Puts all items to beginning of array
        Item[] temp = new Item[InventorySize];
        foreach (Item? item in Inventory)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == null)
                {
                    if (item != null)
                    { temp[i] = item; }
                    break;
                }
            }
        }
        Array.Clear(Inventory, 0, Inventory.Length);
        Inventory = temp;

        string txt = "";

        // Checking inventory: 
        for (int i = 0; i < InventorySize; i++)
        {
            txt += $"[{i + 1}] [{Inventory.ElementAtOrDefault(i)?.Name ?? "Empty Slot"}]\n";
        }
        return txt;
    }
    public string CheckEquipped()
    {
        string txt = "";

        for (int i = 0; i < Equipped.Length; i++)
        {
            switch (i)
            {
                case 0:
                    txt += $"[{i + 1}] [{Equipped.ElementAtOrDefault(i)?.Name ?? "Primary Weapon - Empty Slot"}]\n";
                    break;
                case 1:
                    txt += $"[{i + 1}] [{Equipped.ElementAtOrDefault(i)?.Name ?? "Off Hand       - Empty Slot"}]\n";
                    break;
                case 2:
                    txt += $"[{i + 1}] [{Equipped.ElementAtOrDefault(i)?.Name ?? "Consumable     - Empty Slot"}]\n";
                    break;
            }
        }
        return txt;
    }
    public void UnEquipItem(Item item)
    {
        void UnEquip(Item item)
        {
            for (int i = 0; i < Equipped.Length; i++)
            {
                if (Equipped[i] == item)
                {
                    Debug.Assert(Equipped[i] != null);
                    AddItem(Equipped[i]!);
                    Equipped[i] = null;
                    Utility.Success(item.Name + " unequipped!");
                    for (int j = 0; j < Inventory.Length; j++)
                    {
                        if (Inventory[i] == null)
                        {
                            Inventory[i] = item;
                        }
                    }
                    break;
                }
            }
        }
        if(item.Type == "weapon")
        {
            if (item.Equipped)
            {
                UnEquip(item);
                item.Equipped = false;
                this.Dmg -= item.Dmg;
            }
        }
    }
    public void EquipItem(Item item)
    {
        void Equip(Item item)
        {
            for (int i = 0; i < Equipped.Length; i++)
            {
                if (Equipped[i] != null)
                {
                    AddItem(Equipped[i]!);
                    Equipped[i] = item;
                    break;
                }
                else if (Equipped[i] == null)
                {   
                    Equipped[i] = item;
                    break;
                }
            }
            for (int i = 0; i<Inventory.Count(); i++)
            {
                if (Inventory[i] == item)
                {
                    Inventory[i] = null;
                }
            }
            Utility.Success(item.Name + " equipped!");
        }
        switch (item.Type)
        {
            case "weapon":
                if (!item.Equipped)
                {
                    Equip(item);
                    item.Equipped = true;
                    this.Dmg += item.Dmg;
                }
                break;
            case "consumable":
                this.Hp += item.Value;
                if (this.Hp > this.MaxHP)
                {
                    this.Hp = this.MaxHP;
                }
                Equip(item);
                break;
        }
    }
    public override void TakeTurn(Entity opponent)
    {
        base.TakeTurn(opponent);
        Console.WriteLine(this.Name + "'s turn!");

    }
}