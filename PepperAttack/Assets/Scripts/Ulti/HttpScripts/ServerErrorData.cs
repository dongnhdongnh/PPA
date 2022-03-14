using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerErrorData
{
    public static Dictionary<string, string> Dics;
    static ServerErrorData _instance;
    public static ServerErrorData Instance
    {
        get
        {
            _instance = new ServerErrorData();
            Dics = new Dictionary<string, string>();
            Dics.Add("LOGIN_FAILED", "Username or password is invalid");
            return _instance;
        }
    }

    public string GetServerError(string code)
    {
        string value = code;
        Dics.TryGetValue(code, out value);
        return value;
    }
}
