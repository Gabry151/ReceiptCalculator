class User
{
    private string name;


    public User(string name)
    {
        this.name = name;
    }

    public string GetName() { return name; }
    public void SetName(string name) { this.name = name; }

    public float GetTotalOwnTo(string name)
    {
        float result = 0f;

        foreach (Receipt receipt in ReceiptList.Get())
        {
            if (receipt.GetPayer() != name) continue;
            result += receipt.GetTotalOf(GetName());
        }

        return result;
    }

    public string Show()
    {

        string result = GetName() + ":";

        foreach (string user in UserList.GetAllNames())
        {
            float temp = GetTotalOwnTo(user);
            if (temp <= 0f) continue;
            if (GetName() == user) continue;    //skippa se è la stessa persona a cui dare soldi

            result += "\n" + GetName() + " -> " + user +": ";
            result += temp.ToString("C");
        }

        return result;
    }
}