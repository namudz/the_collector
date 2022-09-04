namespace InterfaceAdapters.Services
{
    public interface IJsonParser
    {
        T FromJson<T>(string json);
        string ToJson(object obj);
    }
}