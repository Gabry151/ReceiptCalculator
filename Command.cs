class Command
{

    public static string[] GetWords(string line)
    {
        //se la linea è vuota, torna vuoto
        if (String.IsNullOrEmpty(line)) { return []; }

        line = line.ToUpper();

        string[] words = line.Split(' ');

        return words;
    }

    public static string GetCommand(string line)
    {
        //separa la line
        string[] words = GetWords(line);

        //se non c'è un comando o è vuoto, torna ERR
        if (words.Length < 1 || String.IsNullOrEmpty(words[0])) { return "ERR"; }

        return words[0];
    }

    public static string[] GetParams(string line)
    {
        //separa la line
        string[] words = GetWords(line);

        //se la linea è vuota, torna 0 args.
        if (words.Length < 2 || String.IsNullOrEmpty(line)) { return []; }

        int paramsCount = words.Length -1;

        string[] prms = new string[paramsCount];

        for (int i = 0; i < paramsCount; i++)
        {
            prms[i] = words[i+1];
        }

        return prms;
    }
}