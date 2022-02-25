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

    public void MyTeamUpdate(string pp1, string pp2, string pp3, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        string _jsonOne = new string[] {
        "pp1",pp1,"pp2",pp2,"pp3",pp3
        }.toJSON();
        httpRESTController.POST(mono, "team", true, _jsonOne, null, null, onSuccess, onError);
    }
}
