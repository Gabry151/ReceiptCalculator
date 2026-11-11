class Program
{
    public static bool blockInput;

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        Menu.HomeMenu();
    }

    //--------------------------COMMANDS

    public static void Wait4Input()
    {
        if (blockInput) return;

        Writer.SeparatorLine(0);
        Console.Write(">");
        string input = Console.ReadLine();

        PerformInput(input);

        Wait4Input();
    }

    public static void PerformInput(string input)
    {
        string command = Command.GetCommand(input);
        string[] prms = Command.GetParams(input);

        switch (Menu.openedMenu)
        {
            case MenuType.Home:
                HomeMenuCommands(command, prms);
                break;
            case MenuType.ReceiptEdit:
                EditMenuCommands(command, prms, Menu.openedReceipt);
                break;
        }
    }

    static void HomeMenuCommands(string command, string[] prms)
    {
        switch (command)
        {
            case Vocabulary.CLOSE:
                Cls();
                return;

            case Vocabulary.CLEAR:
                Menu.HomeMenu();
                return;

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

            case Vocabulary.HELP:
                Hlp();
                break;

            default:
                Writer.NewLine("ERROR: no command. Type HLP for a list of commands.");
                break;
        }
    }

    static void EditMenuCommands(string command, string[] prms, Receipt receipt)
    {
        switch (command)
        {
            case Vocabulary.CLOSE:
                Cls();
                break;

            case Vocabulary.CLEAR:
                Menu.ReceiptEditMenu(receipt);
                return;

            case Vocabulary.EXIT:
                Menu.HomeMenu();
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

            case Vocabulary.PAYER:
                Pyr(receipt, prms);
                break;

            case Vocabulary.SETTLED:
                Stl(receipt, prms);
                break;

            default:
                Writer.NewLine("ERROR: no command. Type HLP in HomeMenu for a list of commands.");
                break;
        }

        Menu.ReceiptEditMenu(receipt);
    }

    //--------------------------CONSOLE COMMANDS

    static bool CheckParamErrors(string[] prms, int limInf)
    {
        if (prms.Length < limInf)
        {
            Writer.NewLine("ERROR: not enough parameters.");
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
            Writer.NewLine("ERROR: too many parameters.");
            return true;
        }
        return false;
    }

    //--------------------------------------------------------------------

    static void Cls()
    {
        Console.ResetColor();
        Environment.Exit(0);
    }

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
                        Writer.NewLine("User " + names[i] + " successfully created.");
                    }
                    else
                    {
                        Writer.NewLine("User " + names[i] + " can't be created.");
                    }
                }

                break;

            case Vocabulary.RECEIPT:

                temp = ReceiptList.Add(names);

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i])
                    {
                        Writer.NewLine("Receipt " + names[i] + " successfully created.");
                    }
                    else
                    {
                        Writer.NewLine("Receipt " + names[i] + " can't be created.");
                    }
                }

                break;

            default:
                Writer.NewLine("ERROR: unexisting parameters.");
                break;
        }


    }

    static void Del(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        // se DEL ALL cancella tutti i dati
        if (prms.Length == 1)
        {
            if (prms[0] == Vocabulary.ALL)
            {
                Save.ClearAllData();
                Writer.NewLine("All data successfully removed.");
            }
            else
            {
                Writer.NewLine("ERROR: unexisting parameters.");
            }

            return;
        }

        string name = prms[1];

        switch (prms[0])
        {
            case Vocabulary.USER:
                //se è all
                if (name == Vocabulary.ALL)
                {
                    UserList.ClearList();
                    Writer.NewLine("All users successfully removed.");
                    break;
                }
            
                if (UserList.Remove(name))
                {
                    Writer.NewLine("User " + name + " successfully removed.");
                }
                else
                {
                    Writer.NewLine("User " + name + " doesn't exist.");
                }
                break;

            case Vocabulary.RECEIPT:
                //se è all
                if (name == Vocabulary.ALL)
                {
                    ReceiptList.ClearList();
                    Writer.NewLine("All receipts successfully removed.");
                    break;
                }

                if (ReceiptList.Remove(name))
                {
                    Writer.NewLine("Receipt " + name + " successfully removed.");
                }
                else
                {
                    Writer.NewLine("Receipt " + name + " doesn't exist.");
                }
                break;

            default:
                Writer.NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Lst(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        string type = prms[0];
        string type2 = Vocabulary.ALL;

        if (prms.Length == 2)
        {
            type2 = prms[1];
        }

        switch (type)
        {
            case Vocabulary.USER:
                Writer.NewLine(UserList.Show());
                break;

            case Vocabulary.RECEIPT:
                Writer.NewLine(ReceiptList.Show());
                break;
            
            case Vocabulary.SAVE:
                Writer.NewLine(Save.GetSaveList(type2));
                break;

            case Vocabulary.LOAD:
                Writer.NewLine(Save.GetSaveList(type2));
                break;

            default:
                Writer.NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Opn(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        string name;
        string type;

        if (prms.Length == 1)
        {
            name = prms[0];
            type = Vocabulary.RECEIPT;
        }
        else
        {
            type = prms[0];
            name = prms[1];
        }


        switch (type)
        {
            case Vocabulary.USER:

                Writer.NewLine(UserList.GetUserByName(name)?.Show());

                break;

            case Vocabulary.RECEIPT:

                Receipt receipt = ReceiptList.GetReceiptByName(name);

                //check if receipt exist
                if (receipt == null)
                {
                    Writer.NewLine("Receipt " + name + " doesn't exist.");
                    return;
                }

                Menu.ReceiptEditMenu(receipt);

                break;

            default:
                Writer.NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Sav(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        string type;
        string name;

        if (prms.Length == 1)
        {
            type = Vocabulary.ALL;
            name = prms[0];
        }
        else
        {
            type = prms[0];
            name = prms[1];
        }

        switch (type)
        {
            case Vocabulary.USER:
                break;

            case Vocabulary.RECEIPT:
                Receipt receipt = ReceiptList.GetReceiptByName(name);
                if (receipt != null)
                {
                    if (Save.SaveReceipt(receipt))
                    {
                        Writer.NewLine("Saved successfully in <<" + name + ".txt>>!");
                    }
                    else
                    {
                        Writer.NewLine("Save error!");
                    }
                }
                else
                {
                    Writer.NewLine("ERROR: this receipt doesn't exist!");
                }
                break;
            
            case Vocabulary.ALL:
                if (Save.SaveAll(name))
                {
                    Writer.NewLine("Saved successfully in <<" + name + ".txt>>!");
                }
                else
                {
                    Writer.NewLine("Save error!");
                }
                break;

            default:
                Writer.NewLine("ERROR: unexisting parameters.");
                break;
        }
    }

    static void Lod(string[] prms)
    {
        if (CheckParamErrors(prms, 1, 2)) return;

        string type;
        string name;

        if (prms.Length == 1)
        {
            type = Vocabulary.ALL;
            name = prms[0];
        }
        else
        {
            type = prms[0];
            name = prms[1];
        }

        if (Save.DoLoad(type, name))
        {
            Writer.SeparatorLine(0);
            Writer.NewLine("<<" + name + ".txt>> loaded successfully!");
        }
        else
        {
            Writer.NewLine("Load error!");
        }
    }

    static void Hlp()
    {
        Writer.NewLine("", false);

        Writer.NewLine("HOME MENU - COMMANDS:");
        Writer.NewLine("|" + Vocabulary.NEW + " [type] [name] - Create a new obj of type");
        Writer.NewLine("|" + Vocabulary.DELETE + " [type] [name] - Delete a created obj of type");
        Writer.NewLine("|" + Vocabulary.OPEN + " [type] [name] - Open the type by name");
        Writer.NewLine("|" + Vocabulary.LIST + " [type] - Show a list of all created obj of type");
        Writer.NewLine("|" + Vocabulary.SAVE + " [name] - Save all in a file");
        Writer.NewLine("|" + Vocabulary.LOAD + " [name] - Load a save file");
        Writer.NewLine("|" + Vocabulary.CLEAR + " - Clear the console outputs");
        Writer.NewLine("|" + Vocabulary.CLOSE + " - Close the application");
        Writer.NewLine("|" + Vocabulary.HELP + " - List all commands.");

        Writer.NewLine("", false);

        Writer.NewLine("RECEIPT EDIT MENU - COMMANDS:");
        Writer.NewLine("|" + Vocabulary.ADD + " [amount] [users] - Add an entry of amount to user");
        Writer.NewLine("|" + Vocabulary.REMOVE + " [entry] - Remove the entry");
        Writer.NewLine("|" + Vocabulary.DISCOUNT + " [amount] [entry] - Add a discount to the entry of amount");
        Writer.NewLine("|" + Vocabulary.PAYER + " [user] - Register the user who payed for the receipt");
        Writer.NewLine("|" + Vocabulary.SETTLED + " [user] - Register who settled the payment");
        Writer.NewLine("|" + Vocabulary.EXIT + " - Return to the HomeMenu");
        Writer.NewLine("|" + Vocabulary.CLOSE + " - Close the application");
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

    static void Pyr(Receipt receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 1, 1)) return;
        
        string name = prms[0];

        if (!UserList.Contain(name)) return;

        receipt.SetPayer(name);
    }

    static void Stl(Receipt receipt, string[] prms)
    {
        if (CheckParamErrors(prms, 1)) return;

        string[] names = prms;

        foreach (string name in names)
        {
            if (name == Vocabulary.ALL)
            {
                foreach (string user in receipt.GetAllUsers())
                {
                    receipt.AddSettledUser(user);
                }
                return;
            }

            if (!UserList.Contain(name)) continue;

            if (receipt.HasSettled(name))
            {
                receipt.RemoveSettledUser(name);
            }
            else
            {
                receipt.AddSettledUser(name);
            }
        }
    }
}