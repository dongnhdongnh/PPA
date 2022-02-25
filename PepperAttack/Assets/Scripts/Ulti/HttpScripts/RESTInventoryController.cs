using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESTInventoryController : IRESTController
{
    public void AllItems(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "inventory/hp", null, onSuccess, onError);
    }

    public void UseItem(string pepper_id, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        string _jsonOne = new string[] {
        "pepper_id",pepper_id
        }.toJSON();
        httpRESTController.POST(mono, "inventory/hp/use", true, _jsonOne, null, null,onSuccess, onError);
    }
}
