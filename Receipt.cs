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

    public Entry GetEntry(int index) { return entries[index]; }


    
}