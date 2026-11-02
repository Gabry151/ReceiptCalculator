class Receipt1
{
    private string name;
    private List<User> users = new();
    private List<Entry> entries = new();
    private float totalDiscount = 0f;


    public Receipt1(string name)
    {
        this.name = name;
        foreach (User user in UserList.Get())
        {
            users.Add(new User(user.GetName()));
        }
    }

    public string GetName() { return name; }
    public void SetName(string name) { this.name = name; }

    public List<Entry> GetEntries() { return entries; }

    public float GetTotalDiscount() { return totalDiscount; }
    public void SetTotalDiscount(float amount) { totalDiscount = Math.Abs(amount); }

    public void Add(Entry entry)
    {
        entries.Add(entry);
    }

    public void Remove(Entry entry)
    {
        entries.Remove(entry);
    }

    public string Show()
    {
        string temp = "list:";
         
        if (entries.Count < 1) return "empty";

        for (int i = 0; i < entries.Count; i++)
        {
            temp += "\n";
            temp += i + ") ";
            temp += entries[i].GetAmount().ToString("C");
            temp += entries[i].GetDiscount() > 0f ? " - " + entries[i].GetDiscount().ToString("C") : "";
            temp += " |";

            foreach (string user in entries[i].GetUsers())
            {
                temp += " " +  user;
            }
        }

        temp += "\n__________\n";

        if (totalDiscount > 0f)
        {
            temp += "- " + totalDiscount + "%";
        }

        foreach (User user in users)
        {
            user.SetAmount(0);

            foreach (Entry entry in entries)
            {
                if (!entry.ContainUser(user.GetName())) { continue; }

                if (entry.GetDiscount() > 0f)
                {
                    user.AddAmount((entry.GetAmount() - entry.GetDiscount()) / entry.GetUsers().Count);
                }
                else if (totalDiscount > 0f)
                {
                    user.AddAmount((entry.GetAmount() - (entry.GetAmount() * totalDiscount / 100f)) / entry.GetUsers().Count);
                }
                else
                {
                    user.AddAmount(entry.GetAmount() / entry.GetUsers().Count);
                }
            }

            temp += " | " + user.GetName() + " = " + user.GetAmount().ToString("C");
        }

        float tot = 0f;

        foreach (Entry entry in entries)
        {
            tot += entry.GetAmount();

            if (entry.GetDiscount() > 0f)
            {
                tot -= entry.GetDiscount();
            }
            else if (totalDiscount > 0f)
            {
                tot -= entry.GetAmount() * totalDiscount / 100f;
            }
        }

        temp += "\nTOT: " + tot.ToString("C");

        return temp;
    }
}