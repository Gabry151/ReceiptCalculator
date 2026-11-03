class Receipt
{
    private string name;
    private List<Entry> entries = new();
    private float totalDiscount = 0f;


    public Receipt(string name)
    {
        this.name = name;
    }

    public string GetName() { return name; }
    public void SetName(string name) { this.name = name; }

    public float GetTotalDiscount() { return totalDiscount; }
    public void SetTotalDiscount(float amount) { totalDiscount = amount; }

    public List<Entry> GetEntries() { return entries; }
    public Entry GetEntry(int index) { return entries[index]; }

    public void Add(Entry entry)
    {
        entries.Add(entry);
    }

    public void Remove(Entry entry)
    {
        entries.Remove(entry);
    }


    public float GetTotalOf(string name)
    {
        float result = 0f;

        foreach (Entry entry in entries)
        {
            if (!entry.ContainUser(name)) continue;

            result += entry.GetUserPortion(name, GetTotalDiscount());
        }

        return result;
    }

    public float GetTotalFromEntries()
    {
        float result = 0f;

        foreach (Entry entry in entries)
        {
            result += entry.GetFinalAmount(totalDiscount);
        }

        return result;
    }

    public float GetTotalFromUsers()
    {
        float result = 0f;

        foreach (string user in UserList.GetAllNames())
        {
            result += GetTotalOf(user);
        }

        return result;
    }

    public string Show()
    {
        string result = "";

        if (entries.Count <= 0) { return result + "\nEmpty!"; }

        // i) amount | usr1 usr2 ...
        for (int i = 0; i < entries.Count; i++)
        {
            result += "\n";
            result += i + ") ";
            result += GetEntry(i).Show();
        }

        result += "\n__________\n";

        // scrivi il total discount
        if (totalDiscount > 0f)
        {
            result += "- " + totalDiscount + "%";
        }
        
        // scrivi le divisioni
        foreach (string user in UserList.GetAllNames())
        {
            float temp = GetTotalOf(user);
            if (temp == 0f) continue;
            result += " | " + user + " = " + temp.ToString("C");
        }

        // scrivi il totale
        result += "\nTOT: ";
        result += GetTotalFromUsers().ToString("C");

        if (GetTotalFromEntries().ToString("C") != GetTotalFromUsers().ToString("C"))
        {
            result += " / " + GetTotalFromEntries().ToString("C");
        }

        return result;
    }
}