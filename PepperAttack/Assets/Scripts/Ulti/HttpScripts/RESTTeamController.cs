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

    public void MyTeamUpdate(List<string> pps, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        string _jsonOne = new string[] {
        "pp1",pps[0],"pp2",pps[1],"pp3",pps[2]
        }.toJSON();
        httpRESTController.POST(mono, "team", true, _jsonOne, null, null, onSuccess, onError);
    }
}
