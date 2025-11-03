namespace Game;
using System.Diagnostics;
/// <summary>
/// Represents the player-controlled <see cref="Actor"/>.
/// </summary>
/// <remarks>
/// The <see cref="Player"/> manages an inventory and equipped items and
/// provides helper methods to inspect, equip, and unequip items. Equipment
/// affects player stats (for example, equipping a <see cref="Weapon"/>
/// modifies <see cref="Actor.Dmg"/>).
/// </remarks>
class Player : Actor
{
    /// <summary>
    /// The currently equipped items. Index mapping:
    /// [0] Primary weapon, [1] Off-hand, [2] Consumable.
    /// Elements may be <c>null</c> when the slot is empty.
    /// </summary>
    public Item?[] Equipped = new Item?[3];
    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="name">Player's display name.</param>
    /// <param name="maxHP">Maximum health points for the player.</param>
    /// <param name="mp">Mana/energy points for the player.</param>
    /// <param name="dmg">Base damage value for the player.</param>
    /// <param name="xp">Starting experience points.</param>
    /// <param name="lvl">Starting level.</param>
    /// <param name="inventorySize">Size of the player's inventory (number of slots).</param>
    public Player(string name, double maxHP, int mp, double dmg, int xp, int lvl, int inventorySize)
                : base(name, maxHP, mp, dmg, xp, lvl, inventorySize)
    { }
    /// <summary>
    /// Shows the player's inventory to the console. When <paramref name="equip"/>
    /// is <c>true</c>, the method will prompt the user to select an item and
    /// attempt to equip it.
    /// </summary>
    /// <param name="equip">If <c>true</c>, allow selecting and equipping an item; otherwise only display inventory.</param>
    public void CheckInventory(bool equip = false)
    {
        string Display()
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

            string txt = "--Your Inventory--\n";

            // Checking inventory: 
            for (int i = 0; i < InventorySize; i++)
            {
                txt += $"[{i + 1}] [{Inventory.ElementAtOrDefault(i)?.Name ?? "Empty Slot"}]\n";
            }
            return txt;
        }
        if (equip)
        {
            string choice = Utility.Prompt(Display());
            if (string.IsNullOrWhiteSpace(choice)) { return; }
            int.TryParse(choice, out int nr);
            if (nr - 1 > Inventory.Length) { Utility.Error("No item selected!"); return; }
            if (this.Inventory[nr - 1] != null)
            {
                try { Console.Clear(); } catch { }
                Console.WriteLine(this.Inventory[nr - 1]!.Info());

                choice = Utility.Prompt("Equip?(y/n)", clear: false);
                if (string.IsNullOrWhiteSpace(choice)) { return; }
                if (choice == "y") { this.EquipItem(this.Inventory[nr - 1]!); }
                else { return; }
            }
            else
            {
                Utility.Error("No item selected!");
            }
        }
        else
        {
            Console.WriteLine(Display());
        }
    }
    public void CheckEquipped()
    {
        /// <summary>
        /// Displays the currently equipped items and allows the user to
        /// select a slot to view details and optionally unequip the item.
        /// </summary>
        
        string Display()
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
        string choice = Utility.Prompt(Display());
        if (string.IsNullOrWhiteSpace(choice)) { return; }
        int.TryParse(choice, out int nr);
        if (nr-1 > Equipped.Length) {Utility.Error("No item selected!"); return; }
        if (this.Equipped[nr - 1] != null)
        {
            try{Console.Clear();} catch{}
            Console.WriteLine(this.Equipped[nr - 1]!.Info());

            choice = Utility.Prompt("Unequip?(y/n)", clear: false);
            if (string.IsNullOrWhiteSpace(choice)) { return; }
            if (choice == "y") { this.UnEquipItem(this.Equipped[nr - 1]!); }
            else { return; }
        }
        else
        {
            Utility.Error("No item selected!");
        } 
    }
    /// <summary>
    /// Unequips <paramref name="item"/> if it is currently equipped.
    /// </summary>
    /// <param name="item">The item to unequip. If the item is a <see cref="Weapon"/>, the player's damage will be adjusted.</param>
    /// <remarks>
    /// The method will attempt to move the unequipped item back into the inventory
    /// and update related stats (for example, decrease damage when a weapon is removed).
    /// </remarks>
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
        if(item is Weapon)
        {
            UnEquip(item);
            this.Dmg -= item.Value;
        }
    }
    /// <summary>
    /// Equips the provided <paramref name="item"/> into the appropriate slot.
    /// </summary>
    /// <param name="item">Item to equip. If the item is a <see cref="Consumable"/>, it is consumed and its effect applied immediately.</param>
    /// <remarks>
    /// For <see cref="Weapon"/> items this method updates the player's damage.
    /// When equipping, any currently-equipped item in the target slot will be
    /// unequipped first.
    /// </remarks>
    public void EquipItem(Item item)
    {
        void Equip(Item item)
        {
            for (int i = 0; i < Equipped.Length; i++)
            {
                if (item is not Consumable)
                {
                    if (Equipped[i] != null)
                    {
                        UnEquipItem(Equipped[i]!);
                        Equipped[i] = item;
                        break;
                    }
                    else if (Equipped[i] == null)
                    {
                        Equipped[i] = item;
                        break;
                    }
                }
            }
            for (int i = 0; i < Inventory.Count(); i++)
            {
                if (Inventory[i] == item)
                {
                    Inventory[i] = null;
                }
            }
        }
        switch (item)
        {
            case Weapon:
                Equip(item);
                this.Dmg += item.Value;
                Utility.Success(item.Name + " equipped!");
                break;
            case Consumable:
                double restoredHP = item.Value;
                double leftOverHP = 0;
                this.Hp += item.Value;
                if (this.Hp > this.MaxHP)
                {
                    leftOverHP = this.Hp - MaxHP;
                    this.Hp = this.MaxHP;
                }
                restoredHP = restoredHP - leftOverHP;
                Equip(item);
                Utility.Success($"{this.Name} restored {restoredHP}!");
                break;
        }
    }
    /// <summary>
    /// Called when it is the player's turn in combat. This override currently
    /// prints a turn announcement and defers to <see cref="Actor.TakeTurn"/> for shared behavior.
    /// </summary>
    /// <param name="opponent">The current opponent <see cref="Entity"/> in the encounter.</param>
    public override void TakeTurn(Entity opponent)
    {
        base.TakeTurn(opponent);
        Console.WriteLine(this.Name + "'s turn!");

    }
}