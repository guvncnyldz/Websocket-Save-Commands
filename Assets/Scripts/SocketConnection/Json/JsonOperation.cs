using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JsonOperation : MonoBehaviour
{
    public void ProccessJson(JObject jObject)
    {
        try
        {
            ServerMessage serverMessage = ServerMessage.ParseData(jObject);

            Type type = Type.GetType(serverMessage.@class);
            MethodInfo methodInfo = type.GetMethod(serverMessage.method);

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            Player player = PlayerListener.GetInstance().FindPlayer(Convert.ToInt16(serverMessage.key));

            List<object> functionParameters =
                ServerMessageUtils.CreateFunctionParameters(parameterInfos, serverMessage);

            Dispatcher.Instance.Invoke(() => InvokeMethod(methodInfo, player, functionParameters));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    void InvokeMethod(MethodInfo methodInfo, Player player, List<object> functionParameters)
    {
        methodInfo.Invoke(player, functionParameters.ToArray());
    }

    JToken ExtractJValue(string key, JObject data)
    {
        JToken token;
        if (data.TryGetValue(key, out token))
            return token;

        return null;
    }
}