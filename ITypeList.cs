interface ITypeList<T>
{
    static abstract List<T> Get();
    static abstract T GetElement(string name);
    static abstract bool Contain(string name);
    static abstract bool Add(string name);
    static abstract bool[] Add(string[] name);
    static abstract bool Remove(string name);
    static abstract bool[] Remove(string[] name);
    static abstract List<string> GetAllNames();
    static abstract void ClearList();
    static abstract string Show();
}