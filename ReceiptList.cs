class ReceiptList
{
    private static List<Receipt1> receiptList = new();


    public static List<Receipt1> Get() { return receiptList; }
    public static bool Add(string name)
    {
        if (GetReceiptByName(name) != null)
        {
            return false;
        }

        receiptList.Add(new Receipt1(name));
        return true;
    }

    public static bool Remove(string name)
    {
        Receipt1 receipt = GetReceiptByName(name);
        if (receipt == null) return false;

        receiptList.Remove(receipt);
        return true;
    }

    public static Receipt1 GetReceiptByName(string name)
    {
        foreach (Receipt1 receipt in receiptList)
        {
            if (receipt.GetName() == name)
            {
                return receipt;
            }
        }

        return null;
    }

    public static string Show()
    {
        string temp = "List of all receipts:";

        foreach (Receipt1 receipt in receiptList)
        {
            temp += "\n";
            temp += "-" + receipt.GetName();
        }

        return temp;
    }
}