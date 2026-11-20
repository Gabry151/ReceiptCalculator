abstract class ElementType
{
    private string name;

    public string GetName() { return name; }
    public bool SetName(string name)
    {
        if (!Vocabulary.IsAllowed(name)) return false;

        this.name = name;
        return true;
    }

    public abstract string Show();
}