using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RESTDebugData", menuName = "Assets/Scriptable Objects/RESTDebugData")]
public class RESTDebugData : ScriptableObject
{
    public List<RESTDebugObject> debugObjects;
}

[System.Serializable]
public class RESTDebugObject
{
    public string method;
    public string url;
    [Multiline]
    public string data;
    [Multiline]
    public string res;

    public bool ShowDetail { get; set; }
}
