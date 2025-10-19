namespace Game;

class Item
{
    public string Name;
    public int Value;

    public Item(string name, int value)
    {
        Name = name;
        Value = value;

    }
    public string Info()
    {
        if (this is Weapon)
        {
            return  $"Name:   [{Name}]\n" +
                    $"Damage: [{Value}]";
        }
        else if (this is Consumable)
        {
            return $"Name:   [{Name}]\n" +
                   $"HP:     [{Value}]";
        }
        else
        {
            return "Unknown item";
        }
    }
}