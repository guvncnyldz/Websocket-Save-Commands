using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;

public class MakeJsonData
{
    private string type;
    private string method;
    private string key;
    private List<string> arguments;

    private MethodCallExpression methodExpression;
    public MakeJsonData(Expression<Action> action)
    {
        SetFields(action);
    }
    
    public MakeJsonData(Expression<Action> action, int key)
    {
        SetFields(action);

        this.key = key.ToString();
    }

    public JObject Make()
    {
        JObject jObject = new JObject();

        jObject.Add("Type", type);
        jObject.Add("Method", method);
        
        if (key != null)
            jObject.Add("key", key);

        List<string> methodParametersName = GetMethodParametersName();

        for (int i = 0; i < arguments.Count; i++)
        {
            jObject.Add(methodParametersName[i],arguments[i]);
        }
        
        return jObject;
    }

    void SetFields(Expression<Action> action)
    {
        methodExpression = (MethodCallExpression) action.Body;

        type = methodExpression.Method.DeclaringType.ToString();
        method = methodExpression.Method.Name;

        arguments = new List<string>();
        
        foreach (var argument in methodExpression.Arguments)
        {
            arguments.Add(argument.ToString());
        }
    }
    
    List<string> GetMethodParametersName()
    {
        MethodInfo methodInfo = methodExpression.Method;

        ParameterInfo[] parameterInfos = methodInfo.GetParameters();

        List<string> parametersName = new List<string>();
        
        foreach (var parameterInfo in parameterInfos)
        {
            parametersName.Add(parameterInfo.Name);
        }

        return parametersName;
    }
}