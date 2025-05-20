using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base;

public static class SessionExtentions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
    
    public static T? GetObjectFromJson<T>(this ISession session, string key)
    {
        var json = session.GetString(key);
        return json == null ? default : JsonSerializer.Deserialize<T>(json);
    }
}