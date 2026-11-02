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
}