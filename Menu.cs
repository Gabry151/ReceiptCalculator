public enum MenuType
{
    Home,
    ReceiptEdit,
    User,
}

class Menu
{
    public static MenuType openedMenu;
    public static Receipt openedReceipt;

    public static void HomeMenu()
    {
        openedMenu = MenuType.Home;

        Program.NewLine("COMMANDS:", true);
        Program.NewLine("|" + Vocabulary.NEW + " [type] [name] - Create a new obj of type");
        Program.NewLine("|" + Vocabulary.OPEN + " [name] - Open a receipt");
        Program.NewLine("|" + Vocabulary.LIST + " [type] - Show a list of all created obj of type");

        Console.WriteLine();

        Program.NewLine("TYPES:");
        Program.NewLine("|" + Vocabulary.USER + " - User");
        Program.NewLine("|" + Vocabulary.RECEIPT + " - Receipt");

        Program.Wait4Input();
    }

    public static void ReceiptEditMenu(Receipt receipt)
    {
        openedMenu = MenuType.ReceiptEdit;
        openedReceipt = receipt;

        Program.NewLine("EDIT RECEIPT - " + receipt.GetName() + ":", true);

        Program.NewLine(receipt.Show());

        Program.Wait4Input();
    }
}