class Save
{
    public static bool DoSave(string fileName)
    {
        string docPath = Path.Combine(Directory.GetCurrentDirectory(), "save");

        if (!Vocabulary.IsGood(fileName)) return false;

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName + ".txt")))
        {
            outputFile.WriteLine(GetUserSave());

            outputFile.WriteLine(GetReceiptListSave());

            outputFile.WriteLine(Vocabulary.CLEAR);
        }

        return true;
    }

    public static bool DoLoad(string fileName)
    {
        string docPath = Path.Combine(Directory.GetCurrentDirectory(), "save");

        //check if in blacklist
        if (!Vocabulary.IsGood(fileName)) return false;
        
        //check if not contain any invalid characters
        foreach (char c in fileName)
        {
            if (Path.GetInvalidPathChars().Contains(c)) return false;
        }

        ClearAllData();

        Program.blockInput = true;

        using (StreamReader sr = new StreamReader(Path.Combine(docPath, fileName + ".txt")))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                if (line[0] == '#') continue;

                Program.PerformInput(line);
            }
        }

        Program.blockInput = false;

        return true;
    }

    public static void ClearAllData()
    {
        UserList.ClearList();
        ReceiptList.ClearList();
    }

    public static string GetUserSave()
    {
        string result = "# USERS";

        foreach (string user in UserList.GetAllNames())
        {
            result += $"\n{Vocabulary.NEW} {Vocabulary.USER} {user}";
        }

        return result;
    }

    public static string GetReceiptListSave()
    {
        string result = "# RECEIPT_LIST";

        foreach (string receipt in ReceiptList.GetAllNames())
        {
            //crea il receipt
            result += $"\n{Vocabulary.NEW} {Vocabulary.RECEIPT} {receipt}";

            //apri il receipt
            result += $"\n{Vocabulary.OPEN} {Vocabulary.RECEIPT} {receipt}";

            //aggiungi il contenuto
            result += GetReceiptSave(ReceiptList.GetReceiptByName(receipt));

            //chiudi il receipt
            result += $"\n{Vocabulary.EXIT}";
        }

        return result;
    }

    public static string GetReceiptSave(Receipt receipt)
    {
        string result = "\n## RECEIPT: <<" + receipt.GetName() + ">>.";

        foreach (Entry entry in receipt.GetEntries())
        {
            //add [amount] [users]
            result += $"\n{Vocabulary.ADD} {entry.GetAmount()}";
            foreach (string user in entry.GetUsers())
            {
                result += " " + user;
            }

            //aggiungi il discount
            if (entry.GetDiscount() <= 0f) continue;
            result += $"\n{Vocabulary.DISCOUNT} {entry.GetDiscount()}";
        }

        //aggiungi lo sconto totale
        if (receipt.GetTotalDiscount() > 0)
        {
            result += $"\n{Vocabulary.DISCOUNT} {receipt.GetTotalDiscount()} {Vocabulary.ALL}";
        }

        result += "\n## END_RECEIPT: <<" + receipt.GetName() + ">>.";

        return result;
    }
}