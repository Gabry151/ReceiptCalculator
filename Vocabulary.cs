public static class Vocabulary
{
    //COMMANDS
    public const string CLOSE = "CLS";
    public const string CLEAR = "CLR";
    public const string NEW = "NEW";
    public const string DELETE = "DEL";
    public const string LIST = "LST";
    public const string OPEN = "OPN";
    public const string SAVE = "SAV";
    public const string LOAD = "LOD";
    public const string HELP = "HLP";

    //TYPE
    public const string USER = "USR";
    public const string RECEIPT = "RCT";

    //EDIT COMMANDS
    public const string EXIT = "EXT";
    public const string ADD = "ADD";
    public const string REMOVE = "RMV";
    public const string DISCOUNT = "DSC";
    public const string PAYER = "PYR";
    public const string SETTLED = "STL";

    //KEYWORDS
    public const string ALL = "ALL";

    //LISTS
    public static readonly char[] charBlackList =
    {
        '#',
        '.',
    };

    public static readonly string[] wordBlackList =
    {
        ALL,
    };

    public static bool IsGood(string name)
    {
        foreach (string word in wordBlackList)
        {
            if (name == word) return false;
        }

        foreach (char c in name)
        {
            foreach (char d in charBlackList)
            {
                if (c == d) return false;
            }
        }

        return true;
    }
}