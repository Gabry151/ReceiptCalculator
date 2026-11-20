class TypeList<T> where T:ElementType
{
    private static List<T> typeList = new();
    
    public static List<T> Get() { return typeList; }

    public static T GetElement(string name)
    {
        foreach (T element in typeList)
        {
            if (element.GetName() == name)
            {
                return element;
            }
        }

        return null;
    }

    public static bool Contain(string name)
    {
        return GetElement(name) != null;
    }

    public static bool Add(T element)
    {
        //se già esiste annulla
        if (Contain(element.GetName())) return false;

        //se è nella blacklist annulla
        if (!Vocabulary.IsAllowed(element.GetName())) return false;

        typeList.Add(element);
        return true;
    }

    public static bool[] Add(T[] elements)
    {
        bool[] results = new bool[elements.Length];

        for (int i = 0; i < elements.Length; i++)
        {
            results[i] = Add(elements[i]);
        }

        return results;
    }

    public static bool Remove(string name)
    {
        T element = GetElement(name);
        if (element == null) return false;

        typeList.Remove(element);
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

    public static List<string> GetAllNames()
    {
        List<string> temp = new();

        foreach (ElementType element in typeList)
        {
            temp.Add(element.GetName());
        }

        return temp;
    }

    public static string Show()
    {
        string temp = "List of all receipts:";

        foreach (ElementType element in typeList)
        {
            temp += "\n";
            temp += "-|" + element.GetName();
        }

        return temp;
    }

    public static void ClearList()
    {
        typeList.Clear();
    }
}

class ReceiptList : TypeList<Receipt> { }

class UserList : TypeList<User> { }

class KeyList : TypeList<Key> { }