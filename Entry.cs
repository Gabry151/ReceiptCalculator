class Entry
{
    private float amount;
    private float discount;
    private List<string> users = new();

    public Entry(float amount, List<string> users)
    {
        this.amount = amount;

        foreach (string user in users)
        {
            if (!UserList.Contain(user)) continue;
            this.users = users;
        }
    }

    public float GetAmount() { return amount; }
    public void SetAmount(float amount) { this.amount = amount; }

    public List<string> GetUsers() { return users; }

    public float GetDiscount() { return discount; }
    public void SetDiscount(float amount) { discount = amount; }

    public bool ContainUser(string name)
    {
        foreach (string user in users)
        {
            if (user == name) { return true; }
        }
        return false;
    }

    public int ContainUserCounter(string name)
    {
        int i = 0;

        foreach (string user in users)
        {
            if (user == name) i++;
        }

        return i;
    }

    public int GetUsersCount()
    {
        return users.Count;
    }

    public float GetFinalAmount(float totalDiscount)
    {
        float result = GetAmount();

        //se c'è uno sconto applicalo al totale dell'entry
        if (GetDiscount() > 0f)
        {
            result -= GetDiscount();
        }
        else
        //se non c'è uno sconto applica lo sconto totale
        {
            result -= result * totalDiscount / 100f;
        }

        return result;
    }

    public float GetUserPortion(string name, float totalDiscount)
    {
        if (!ContainUser(name)) { return 0f; }

        float result = GetFinalAmount(totalDiscount);
        float divider = GetUsersCount();

        //dividi il risultato per tutti i partecipanti
        result /= divider;

        //moltiplica la porzione per quante volte compare il nome
        result *= ContainUserCounter(name);

        return result;
    }

    public string Show()
    {
        string result = "";

        result += GetAmount().ToString("C");
        result += GetDiscount() > 0f ? " - " + GetDiscount().ToString("C") : "";
        result += " |";

        foreach (string user in GetUsers())
        {
            result += " " + user;
        }

        return result;
    }
}