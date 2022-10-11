using UnityEngine;

public class JsonHelper
{
    public static string GetJsonFromObject<T>(T obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public static T GetObjectFromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
