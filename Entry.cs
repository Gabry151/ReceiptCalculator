class Entry
{
    private float amount;
    private float discount;
    private List<string> users = new();

    public Entry(float amount, List<string> users)
    {
        this.amount = amount;
        this.users = users;
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
}