using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESTBattleController : IRESTController
{
    public void StartMatch(string team_id, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        string _jsonOne = new string[] {
        "team_id",team_id
        }.toJSON();
        httpRESTController.POST(mono, "match/start", true, _jsonOne, null, null,onSuccess, onError);

    }
}
