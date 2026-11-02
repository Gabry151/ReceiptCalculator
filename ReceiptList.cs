class ReceiptList : IAddable
{
    private static List<Receipt> receiptList = new();


    public static List<Receipt> Get() { return receiptList; }

    public static bool Add(string name)
    {
        //se già esiste annulla
        if (GetReceiptByName(name) != null)
        {
            return false;
        }

        //se è nella blacklist annulla
        if (!Vocabulary.IsGood(name))
        {
            return false;
        }

        receiptList.Add(new Receipt(name));
        return true;
    }

    public static bool[] Add(string[] names)
    {
        bool[] results = new bool[names.Length];

        for (int i = 0; i < names.Length; i++)
        {
            results[i] = Add(names[i]);
        }

        return results;
    }

    public static bool Remove(string name)
    {
        Receipt receipt = GetReceiptByName(name);
        if (receipt == null) return false;

        receiptList.Remove(receipt);
        return true;
    }

    public static bool[] Remove(string[] names)
    {
        bool[] results = new bool[names.Length];

        for (int i = 0; i < names.Length; i++)
        {
            results[i] = Remove(names[i]);
        }

        return results;
    }

    public static Receipt GetReceiptByName(string name)
    {
        foreach (Receipt receipt in receiptList)
        {
            if (receipt.GetName() == name)
            {
                return receipt;
            }
        }

        return null;
    }

    public static List<string> GetAllNames()
    {
        List<string> temp = new();

        foreach (Receipt rct in receiptList)
        {
            temp.Add(rct.GetName());
        }

        return temp;
    }

    public static string Show()
    {
        string temp = "List of all receipts:";

        foreach (Receipt receipt in receiptList)
        {
            temp += "\n";
            temp += "-" + receipt.GetName();
        }

        return temp;
    }

    public static void ClearList()
    {
        receiptList.Clear();
    }
}