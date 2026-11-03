class User
{
    private string name;
    private float amount;


    public User(string name)
    {
        this.name = name;
    }

    public string GetName() { return name; }
    public void SetName(string name) { this.name = name; }

    public float GetAmount() { return amount; }
    public void SetAmount(float amount) { this.amount = amount; }
    public void AddAmount(float amount) { SetAmount(this.amount + amount); }

    public float GetTotal()
    {
        float result = 0f;

        foreach (Receipt receipt in ReceiptList.Get())
        {
            result += receipt.GetTotalOf(name);
        }

        return result;
    }

    public string Show()
    {
        string result = name +" TO GAB: ";
        result += GetTotal().ToString("C");

        return result;
    }
}