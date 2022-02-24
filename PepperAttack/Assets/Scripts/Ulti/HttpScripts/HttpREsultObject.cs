using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class HttpREsultObject
{

    public int code;
    public string msg;
    public HttpResultData data;
    public override string ToString()
    {
        //string dataString = "null";
        //if (data != null && data.Length > 0)
        //{
        //    dataString = "Data length " + data.Length;
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        dataString += "\n" + data[i].ToString();
        //    }
        //}
        return "Code:" + code + ",Message:" + msg + ",Data:" + data.ToString();
    }
}

[System.Serializable]
public class HttpResultData
{
    public string token;


    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }


}

