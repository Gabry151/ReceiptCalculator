class UserList
{
    private static List<User> userList = new();


    public static List<User> Get() { return userList; }
    public static bool Add(string name)
    {
        if (GetUserByName(name) != null)
        {
            return false;
        }

        //se il nome è nella lista proibita, ritorna false
        if (name == Vocabulary.ALL)
        {
            return false;
        }
        
        userList.Add(new User(name));
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
        User user = GetUserByName(name);
        if (user == null) return false;
        
        userList.Remove(user);
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


    public static User GetUserByName(string name)
    {
        foreach (User user in userList)
        {
            if (user.GetName() == name)
            {
                return user;
            }
        }

        return null;
    }

    public static List<string> GetAllNames()
    {
        List<string> temp = new();

        foreach (User user in userList)
        {
            temp.Add(user.GetName());
        }

        return temp;
    }

    public static bool Contain(string name)
    {
        foreach (User user in userList)
        {
            if (user.GetName() == name) { return true; }
        }
        return false;
    }

    public static string Show()
    {
        string temp = "List of all users:";

        foreach (User user in userList)
        {
            temp += "\n";
            temp += "-" + user.GetName();
        }

        return temp;
    }
}