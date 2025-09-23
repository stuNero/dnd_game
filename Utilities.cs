namespace Game;
abstract class Utility
{
    public static void GenerateMenu(string title = "Välj Ett Menyval:", params string[] choices)
    {
        string msg = "________________________\n";

        msg += title + "\n";

        for (int i = 0; i < choices.Length; i++)
        {
            msg += $"[{i + 1}] {choices[i]}\n";
        }
        msg += "________________________";
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(msg);
        Console.ResetColor();
    }
    public static void Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(msg);
        Console.ResetColor();
        Console.ReadLine();
    }
    public static void Prompt(string input)
    {
        Console.WriteLine(input + "\n(Enter 'exit' to go back..)");
    }
}