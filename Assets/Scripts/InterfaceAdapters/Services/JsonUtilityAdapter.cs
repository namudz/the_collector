using UnityEngine;

namespace InterfaceAdapters.Services
{
    public class JsonUtilityAdapter : IJsonParser
    {
        public T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string ToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}