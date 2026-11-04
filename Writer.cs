class Writer
{
    public static void NewLine(string line)
    {
        Console.WriteLine("-" + line);
    }

    public static void NewLine(string line, bool indicator)
    {
        if (!indicator)
        {
            Console.WriteLine(line);
        }
        else
        {
            NewLine(line);
        }
    }

    public static void FirstLine(string line)
    {
        Console.Clear();

        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Welcome to ReceiptCalculator v2.0 by Gabry151!");

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        SeparatorLine(3);
        NewLine(line);
    }

    public static void SeparatorLine(int importance)
    {
        string temp = "";

        switch (importance)
        {
            case 0:
                temp = "-----";
                break;
            case 1:
                temp = "----------";
                break;
            case 2:
                temp = "_____";
                break;
            case 3:
                temp = "__________";
                break;
        }

        NewLine(temp, false);
    }
}