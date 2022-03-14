using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESTTeamController : IRESTController
{
    public void AllPepper(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "pepper", null, onSuccess, onError);
    }

    public void MyTeamGet(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "team", null, onSuccess, onError);
    }

    public void Sync(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "sync", null, onSuccess, onError);
    }

    public void MyTeamUpdate(List<string> pps, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        for (int i = pps.Count - 1; i >= 0; i--)
        {
            if (pps[i] == null || pps[i].Length <= 0)
                pps.RemoveAt(i);
        }
        List<string> _output = new List<string>();
        for (int i = 0; i < pps.Count; i++)
        {
            _output.Add("pp" + (i + 1));
            if (pps[i] != null)
                _output.Add(pps[i]);
            else
                _output.Add("");
        }
        string _jsonOne = _output.ToArray().toJSON();
        httpRESTController.POST(mono, "team", true, _jsonOne, null, null, onSuccess, onError);
    }
}
