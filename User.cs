class User : ElementType
{
    public User(string name)
    {
        SetName(name);
    }

    public float GetTotalOwnTo(string name)
    {
        float result = 0f;

        foreach (Receipt receipt in ReceiptList.Get())
        {
            if (receipt.GetPayer() != name) continue;
            if (receipt.HasSettled(GetName())) continue;
            result += receipt.GetTotalOf(GetName());
        }

        return result;
    }

    public override string Show()
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