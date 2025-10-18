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
        string type = "Health Points";
        if (this is Weapon w)
        {
            type = "Damage";
        }
        return  $"Name:     [{Name}]\n"+
                $"{type}:   [{Value}]";
    }
}