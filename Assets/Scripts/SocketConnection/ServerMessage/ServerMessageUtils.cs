using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class ServerMessageUtils
{
    public static string ExtractAndRemoveData(JObject data, string type)
    {
        string value = ExtractData(data, type);
        data.Remove(type);
        return value;
    }

    public static List<object> CreateFunctionParameters(ParameterInfo[] parameters, ServerMessage serverMessage)
    {
        List<object> functionParameters = new List<object>();
        
        JsonSerializerSettings settings = new JsonSerializerSettings();

        foreach (ParameterInfo parameter in parameters)
        {
            if (serverMessage.values.ContainsKey(parameter.Name))
            {
                functionParameters.Add(serverMessage.values[parameter.Name]
                    .ToObject(parameter.ParameterType, JsonSerializer.Create(settings)));
            }
        }

        return functionParameters;
    }

    public static Dictionary<string, JValue> ExtractJValues(JObject data)
    {
        Dictionary<string, JValue> values = new Dictionary<string, JValue>();

        foreach (KeyValuePair<string, JToken> pair in data)
        {
            values.Add(pair.Key, pair.Value.ToObject<JValue>());
        }

        return values;
    }

    public static string ExtractData(JObject data, string keyValue)
    {
        JToken value;
        if (data.TryGetValue(keyValue, out value))
            return value.ToObject<string>();

        return string.Empty;
    }
}