class Program
{
    public static Menu OpenedMenu;
    public static Receipt OpenedReceipt;
    public static bool blockInput;

    static void Main(string[] args)
    {
        HomeMenu();
    }

    //--------------------------COMMANDS

    public static void Wait4Input()
    {
        if (blockInput) return;

        Console.WriteLine("-----");
        Console.Write(">");
        string input = Console.ReadLine();

        PerformInput(input);

        Wait4Input();
    }

    public static void PerformInput(string input)
    {
        string command = Command.GetCommand(input);
        string[] prms = Command.GetParams(input);

        switch (OpenedMenu)
        {
            case Menu.Home:
                HomeMenuCommands(command, prms);
                break;
            case Menu.Edit:
                EditMenuCommands(command, prms, OpenedReceipt);
                break;
        }
    }

    static void HomeMenuCommands(string command, string[] prms)
    {
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
            case Vocabulary.SAVE:
                Sav(prms);
                break;
            case Vocabulary.LOAD:
                Lod(prms);
                break;
            default:
                NewLine("ERROR: no command.");
                break;
        }
    }

    static void EditMenuCommands(string command, string[] prms, Receipt receipt)
    {
        switch (command)
        {
            case Vocabulary.CLOSE:
                Environment.Exit(0);
                break;

            case Vocabulary.CLEAR:
                EditMenu(receipt);
                return;

            case Vocabulary.EXIT:
                HomeMenu();
                return;

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

    //--------------------------MENUS

    static void HomeMenu()
    {
        OpenedMenu = Menu.Home;

        NewLine("COMMANDS:", true);
        NewLine("|" + Vocabulary.NEW + " [type] [name] - Create a new obj of type");
        NewLine("|" + Vocabulary.OPEN + " [name] - Open a receipt");
        NewLine("|" + Vocabulary.LIST + " [type] - Show a list of all created obj of type");

        Console.WriteLine();

        NewLine("TYPES:");
        NewLine("|" + Vocabulary.USER + " - User");
        NewLine("|" + Vocabulary.RECEIPT + " - Receipt");

        Wait4Input();
    }

    static void EditMenu(Receipt receipt)
    {
        OpenedMenu = Menu.Edit;
        OpenedReceipt = receipt;

        NewLine("EDIT RECEIPT - " + receipt.GetName() + ":", true);

        NewLine(receipt.Show());

        Wait4Input();
    }

    //--------------------------CONSOLE COMMANDS

    public static void NewLine(string line)
    {
        Console.WriteLine("-" + line);
    }
    static void NewLine(string line, bool clear)
    {
        Console.Clear();
        Console.WriteLine("Welcome to ReceiptCalculator v2.0 by Gabry151!");
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
        if (CheckParamErrors(prms, 2)) return;

        string[] names = prms.Skip(1).ToArray();
        bool[] temp;

        switch (prms[0])
        {
            case Vocabulary.USER:

                temp = UserList.Add(names);

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i])
                    {
                        NewLine("User " + names[i] + " successfully created.");
                    }
                    else
                    {
                        NewLine("User " + names[i] + " can't be created.");
                    }
                }

                break;

            case Vocabulary.RECEIPT:

                temp = ReceiptList.Add(names);

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i])
                    {
                        NewLine("Receipt " + names[i] + " successfully created.");
                    }
                    else
                    {
                        NewLine("Receipt " + names[i] + " can't be created.");
                    }
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

        Receipt receipt = ReceiptList.GetReceiptByName(name);

        if (receipt == null)
        {
            NewLine("Receipt " + name + " doesn't exist.");
            return;
        }

        EditMenu(receipt);
    }

    static void Sav(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        string name = prms[0];

        if (Save.DoSave(name))
        {
            NewLine("Saved successfully in <<" + name + ".txt>>!");
        }
        else
        {
            NewLine("Save error!");
        }
    }

    static void Lod(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        string name = prms[0];

        if (Save.DoLoad(name))
        {
            Console.WriteLine("-----");
            NewLine("<<" + name + ".txt>> loaded successfully!");
        }
        else
        {
            NewLine("Load error!");
        }
    }


    static void Add(Receipt receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 2)) return;

        prms[0] = prms[0].Replace('.', ',');
        float amount;
        float.TryParse(prms[0], out amount);
        if (amount == 0) return;

        List<string> users = new();

        //se il secondo parametro è "ALL"
        if (prms[1] == Vocabulary.ALL)
        {
            users = UserList.GetAllNames();
        }

        //se il secondo parametro non è "ALL"
        else
        {
            //salta il primo parametro che è il prezzo
            string[] nameParams = prms.Skip(1).ToArray();

            //per ogni parametro user
            foreach (string userName in nameParams)
            {
                //se esiste aggiungilo
                if (!UserList.Contain(userName)) continue;
                users.Add(userName);
            }
        }
        
        if (users.Count <= 0) return;

        Entry entry = new Entry(amount, users);

        receipt.Add(entry);
    }

    static void Rmv(Receipt receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;

        int i = int.Parse(prms[0]);

        if (i >= receipt.GetEntries().Count) return;

        receipt.Remove(receipt.GetEntries()[i]);
    }

    static void Dsc(Receipt receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        //se non ci sono righe, ritorna
        if (receipt.GetEntries().Count <= 0) return;

        //registra il valore
        prms[0] = prms[0].Replace('.', ',');
        float amount;
        float.TryParse(prms[0], out amount);

        //registra a chi si riferisce, se non c'è parametro consideralo all'ultimo
        int i;

        if (prms.Length == 1)
        {
            i = receipt.GetEntries().Count -1;
        }
        else
        {
            if (prms[1] == Vocabulary.ALL)
            {
                i = -1;
            }
            else if (prms[1] == "NDS")
            {
                i = -2;
            }
            else
            {
                int.TryParse(prms[1], out i);

                if (i >= receipt.GetEntries().Count) return;
            }
        }

        //applica lo sconto
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

public enum Menu
{
    Home,
    Edit,
}