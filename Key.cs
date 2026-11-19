class Key
{
    private string name;
    private List<string> commands = new();


    public Key(string name) { this.name = name; }

    public string GetName() { return name; }
    public bool SetName(string name)
    {
        if (!Vocabulary.IsAllowed(name)) return false;

        this.name = name;
        return true;
    }

    public List<string> GetCommands() { return commands; }
    public bool AddCommand(string line)
    {
        commands.Add(line);
        return true;
    }

    public bool RemoveCommand(int i)
    {
        if (commands.Count < i + 1) return false;

        commands.RemoveAt(i);
        return true;
    }

    public string Show()
    {
        string result = "EXECUTE THIS:";

        for (int i = 0; i < commands.Count; i++)
        {
            result += "\n";
            result += i + ") ";
            result += commands[i];
        }

        return result;
    }
}