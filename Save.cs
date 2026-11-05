class Save
{
    public static bool SaveAll(string fileName)
    {
        string docPath = Path.Combine(Directory.GetCurrentDirectory(), Vocabulary.SAVEDIR);

        if (!Path.Exists(docPath)) Directory.CreateDirectory(docPath);

        if (!Vocabulary.IsGood(fileName)) return false;

        //check if not contain any invalid characters
        foreach (char c in fileName)
        {
            if (Path.GetInvalidPathChars().Contains(c)) return false;
        }

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName + ".txt")))
        {
            outputFile.WriteLine(GetUserSave(UserList.GetAllNames()));

            outputFile.WriteLine(GetReceiptListSave());

            outputFile.WriteLine(Vocabulary.CLEAR);
        }

        return true;
    }

    public static bool SaveReceipt(Receipt receipt)
    {
        string docPath = Path.Combine(Directory.GetCurrentDirectory(), Vocabulary.RECEIPTDIR);

        if (!Path.Exists(docPath)) Directory.CreateDirectory(docPath);

        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, receipt.GetName() + ".txt")))
        {
            outputFile.WriteLine(GetUserSave(receipt.GetAllUsers()));

            outputFile.WriteLine(GetCreateReceiptSave(receipt));

            outputFile.WriteLine(Vocabulary.CLEAR);
        }

        return true;
    }

    public static bool DoLoad(string directory, string fileName)
    {
        bool all = false;

        switch (directory)
        {
            case Vocabulary.USER:
                directory = Vocabulary.USERDIR;
                break;
            case Vocabulary.RECEIPT:
                directory = Vocabulary.RECEIPTDIR;
                break;
            case Vocabulary.ALL:
                directory = Vocabulary.SAVEDIR;
                all = true;
                break;
            default:
                return false;
        }

        string docPath = Path.Combine(Directory.GetCurrentDirectory(), directory);

        //check if in blacklist
        if (!Vocabulary.IsGood(fileName)) return false;

        //check if not contain any invalid characters
        foreach (char c in fileName)
        {
            if (Path.GetInvalidPathChars().Contains(c)) return false;
        }

        string filePath = Path.Combine(docPath, fileName + ".txt");

        //return false se il file non esiste
        if (!Path.Exists(filePath)) return false;

        Program.blockInput = true;

        if (all) ClearAllData();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
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
        Menu.HomeMenu();
    }

    public static string GetUserSave(List<string> users)
    {
        string result = "# USERS";

        foreach (string user in users)
        {
            result += $"\n{Vocabulary.NEW} {Vocabulary.USER} {user}";
        }

        return result;
    }

    public static string GetReceiptListSave()
    {
        string result = "# RECEIPT_LIST";

        foreach (Receipt receipt in ReceiptList.Get())
        {
            result += GetCreateReceiptSave(receipt);
        }

        return result;
    }

    public static string GetCreateReceiptSave(Receipt receipt)
    {
        string result = "## NEW RECEIPT";

        //crea il receipt
        result += $"\n{Vocabulary.NEW} {Vocabulary.RECEIPT} {receipt.GetName()}";

        //apri il receipt
        result += $"\n{Vocabulary.OPEN} {Vocabulary.RECEIPT} {receipt.GetName()}";

        //aggiungi il contenuto
        result += GetReceiptSave(receipt);

        //chiudi il receipt
        result += $"\n{Vocabulary.EXIT}";

        result += "\n## END NEW RECEIPT";

        return result;
    }

    public static string GetReceiptSave(Receipt receipt)
    {
        string result = "\n### RECEIPT: <<" + receipt.GetName() + ">>.";

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

        //aggiungi il payer
        if (!string.IsNullOrEmpty(receipt.GetPayer()))
        {
            result += $"\n{Vocabulary.PAYER} {receipt.GetPayer()}";
        }

        //aggiungi i settled
        if (receipt.GetSettledUsers().Count > 0)
        {
            foreach (string user in receipt.GetSettledUsers())
            {
                result += $"\n{Vocabulary.SETTLED} {user}";
            }
        }

        result += "\n## END_RECEIPT: <<" + receipt.GetName() + ">>.";

        return result;
    }

    public static string GetSaveList(string type)
    {
        string result = "";
        string dir = "";

        switch (type)
        {
            case Vocabulary.ALL:
                result = "SAVE LIST:";
                dir = Vocabulary.SAVEDIR;
                break;
            case Vocabulary.USER:
                result = "USER SAVE LIST:";
                dir = Vocabulary.USERDIR;
                break;
            case Vocabulary.RECEIPT:
                result = "RECEIPT SAVE LIST:";
                dir = Vocabulary.RECEIPTDIR;
                break;
            default:
                return "";
        }

        string docPath = Path.Combine(Directory.GetCurrentDirectory(), dir);
        if (!Path.Exists(docPath)) Directory.CreateDirectory(docPath);
        string[] files = Directory.GetFiles(docPath, "*.txt");

        foreach (string file in files)
        {
            result += "\n-|" + Path.GetFileNameWithoutExtension(file);
        }

        return result;
    }
}