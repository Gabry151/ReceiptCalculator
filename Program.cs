class Program
{
    const string Receipt = "RCP";

    static void Main(string[] args)
    {
        HomeMenu();
    }

    static void WaitForInput()
    {
        Console.Write("> ");
        string input = Console.ReadLine();

        string command = Command.GetCommand(input);
        string[] prms = Command.GetParams(input);

        switch (command)
        {
            case Vocabulary.CLOSE:
                Environment.Exit(0);
                break;
            case Vocabulary.CLEAR:
                HomeMenu();
                break;
            case Vocabulary.NEW:
                New(prms);
                break;
            case Vocabulary.DELETE:
                Del(prms);
                break;
            case Vocabulary.LIST:
                Lst(prms);
                break;
            case Vocabulary.OPEN:
                Opn(prms);
                break;
            default:
                NewLine("ERROR: no command.");
                break;
        }

        WaitForInput();
    }

    static void ReceiptEdit(Receipt1 receipt)
    {
        Console.Write("> ");
        string input = Console.ReadLine();

        string command = Command.GetCommand(input);
        string[] prms = Command.GetParams(input);

        switch (command)
        {
            case Vocabulary.CLOSE:
                Environment.Exit(0);
                break;

            case Vocabulary.CLEAR:
                EditMenu(receipt);
                break;

            case Vocabulary.EXIT:
                HomeMenu();
                break;

            case Vocabulary.ADD:
                Add(receipt, prms);
                break;
            case Vocabulary.REMOVE:
                Rmv(receipt, prms);
                break;
            case Vocabulary.DISCOUNT:
                Dsc(receipt, prms);
                break;
            default:
                NewLine("ERROR: no command.");
                break;
        }

        EditMenu(receipt);
    }

    static void HomeMenu()
    {
        NewLine("Waiting for instructions...", true);

        WaitForInput();
    }

    static void EditMenu(Receipt1 receipt)
    {
        NewLine("EDIT RECEIPT - " + receipt.GetName() + ":", true);

        NewLine(receipt.Show());
        
        ReceiptEdit(receipt);
    }

    static void NewLine(string line)
    {
        Console.WriteLine("-" + line);
    }
    static void NewLine(string line, bool clear)
    {
        Console.Clear();
        Console.WriteLine("Welcome to ReceiptCalculator v2.0!");
        Console.WriteLine("-----");
        NewLine(line);
    }

    static bool CheckParamErrors(string[] prms, int limInf)
    {
        if (prms.Length < limInf)
        {
            NewLine("ERROR: not enough parameters.");
            return true;
        }
        return false;
    }
    static bool CheckParamErrors(string[] prms, int limInf, int limSup)
    {
        if (CheckParamErrors(prms, limInf))
        {
            return true;
        }

        if (prms.Length > limSup)
        {
            NewLine("ERROR: too many parameters.");
            return true;
        }
        return false;
    }

    //--------------------------------------------------------------------

    static void New(string[] prms)
    {
        if (CheckParamErrors(prms, 2, 2)) return;

        string name = prms[1];

        switch (prms[0])
        {
            case Vocabulary.USER:
                if (UserList.Add(name))
                {
                    NewLine("User " + name + " successfully added.");
                }
                else
                {
                    NewLine("User " + name + " can't be added.");
                }
                break;

            case Vocabulary.RECEIPT:
                if (ReceiptList.Add(name))
                {
                    NewLine("Receipt " + name + " successfully added.");
                }
                else
                {
                    NewLine("Receipt " + name + " can't be added.");
                }
                break;

            default:
                NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Del(string[] prms)
    {
        if (CheckParamErrors(prms, 2, 2)) return;

        string name = prms[1];

        switch (prms[0])
        {
            case Vocabulary.USER:
                if (UserList.Remove(name))
                {
                    NewLine("User " + name + " successfully removed.");
                }
                else
                {
                    NewLine("User " + name + " doesn't exist.");
                }
                break;

            case Vocabulary.RECEIPT:
                if (ReceiptList.Remove(name))
                {
                    NewLine("Receipt " + name + " successfully removed.");
                }
                else
                {
                    NewLine("Receipt " + name + " doesn't exist.");
                }
                break;

            default:
                NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Lst(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        switch (prms[0])
        {
            case Vocabulary.USER:
                NewLine(UserList.Show());
                break;

            case Vocabulary.RECEIPT:
                NewLine(ReceiptList.Show());
                break;

            default:
                NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Opn(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        string name = prms[0];

        Receipt1 receipt = ReceiptList.GetReceiptByName(name);

        if (receipt == null)
        {
            NewLine("Receipt " + name + " doesn't exist.");
            return;
        }

        EditMenu(receipt);
    }


    static void Add(Receipt1 receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 2)) return;

        prms[0] = prms[0].Replace('.', ',');
        float amount = float.Parse(prms[0]);

        List<string> users = new();

        if (prms[1] == Vocabulary.ALL)
        {
            users = UserList.GetAllNames();
        }
        else
        {
            string[] userNames = prms.Skip(1).ToArray();

            foreach (string userName in userNames)
            {
                User user = UserList.GetUserByName(userName);
                if (!UserList.Contain(userName)) continue;
                users.Add(userName);
            }
        }
        
        if (users.Count <= 0) return;

        Entry entry = new Entry(amount, users);

        receipt.Add(entry);
    }

    static void Rmv(Receipt1 receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        int i = int.Parse(prms[0]);

        if (i >= receipt.GetEntries().Count) return;

        receipt.Remove(receipt.GetEntries()[i]);
    }

    static void Dsc(Receipt1 receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 2, 2)) return;

        int i;

        if (prms[0] == Vocabulary.ALL)
        {
            i = -1;
        }
        else if (prms[0] == "NDS")
        {
            i = -2;
        }
        else
        {
            i = int.Parse(prms[0]);

            if (i >= receipt.GetEntries().Count) return;
        }

        prms[1] = prms[1].Replace('.', ',');
        float amount = float.Parse(prms[1]);

        if (i < 0)
        {
            if (i == -1)
            {
                receipt.SetTotalDiscount(amount);
            }
            if (i == -2)
            {
                receipt.SetTotalDiscount(amount);
            }
        }
        else
        {
            receipt.GetEntries()[i].SetDiscount(Math.Abs(amount));
        }
    }
}