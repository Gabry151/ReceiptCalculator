public enum MenuType
{
    Home,
    ReceiptEdit,
    KeyEdit,
}

class Menu
{
    public static MenuType openedMenu;
    public static Receipt openedReceipt;
    public static Key openedKey;
    

    public static void HomeMenu()
    {
        openedMenu = MenuType.Home;

        Writer.FirstLine("HOME MENU");

        Writer.SeparatorLine(1);

        Writer.NewLine("", false);

        Writer.NewLine("TYPES:");
        Writer.NewLine("|" + Vocabulary.USER + " - User");
        Writer.NewLine("|" + Vocabulary.RECEIPT + " - Receipt");
        Writer.NewLine("|" + Vocabulary.KEY + " - Key");

        Program.Wait4Input();
    }

    public static void ReceiptEditMenu(Receipt receipt)
    {
        openedMenu = MenuType.ReceiptEdit;
        openedReceipt = receipt;

        Writer.FirstLine("EDIT RECEIPT MENU - " + receipt.GetName() + ":");

        Writer.SeparatorLine(1);

        Writer.NewLine(receipt.Show(), false);

        Program.Wait4Input();
    }

    public static void KeyEditMenu(Key key)
    {
        openedMenu = MenuType.KeyEdit;
        openedKey = key;

        Writer.FirstLine("EDIT KEY MENU - " + key.GetName() + ":");

        Writer.SeparatorLine(1);

        Writer.NewLine(key.Show(), false);

        Program.Wait4Input();
    }
}