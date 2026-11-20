class Key : ElementType
{
    private List<string> commands = new();


    public Key(string name) { SetName(name); }

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

    public override string Show()
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