class KeyList : ITypeList<Key>
{
    public static List<Key> keyList = new();

    public static bool Add(string name)
    {
        if (Contain(name)) return false;
        if (!Vocabulary.IsAllowed(name)) return false;

        keyList.Add(new Key(name));
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

    public static void ClearList()
    {
        keyList.Clear();
    }

    public static bool Contain(string name)
    {
        return GetElement(name) != null;
    }

    public static List<Key> Get()
    {
        return keyList;
    }

    public static List<string> GetAllNames()
    {
        List<string> temp = new();

        foreach (Key key in keyList)
        {
            temp.Add(key.GetName());
        }

        return temp;
    }

    public static Key GetElement(string name)
    {
        foreach (Key key in keyList)
        {
            if (key.GetName() == name)
            {
                return key;
            }
        }

        return null;
    }

    public static bool Remove(string name)
    {
        Key key = GetElement(name);
        if (key == null) return false;

        keyList.Remove(key);
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

    public static string Show()
    {
        string temp = "List of all receipts:";

        foreach (Key key in keyList)
        {
            temp += "\n";
            temp += "-|" + key.GetName();
        }

        return temp;
    }
}