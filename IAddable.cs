interface IAddable
{
    static abstract bool Add(string name);
    static abstract bool[] Add(string[] name);
    static abstract bool Remove(string name);
    static abstract bool[] Remove(string[] name);
}