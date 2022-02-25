using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESTUserController : IRESTController
{
    public void Login(string email, string password, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        string _jsonOne = new string[] {
        "email",email,"password",password
        }.toJSON();
        httpRESTController.POST(mono, "auth/login", true, _jsonOne, null, null, (data) =>
        {
            GameRESTController.Instance.TOKEN = data.data.token;

            onSuccess?.Invoke(data);
        }, onError);

    }

    public void Leaderboard(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "rank", null, onSuccess, onError);
    }
    public void Home(Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        httpRESTController.GET(this.mono, "home", null, onSuccess, onError);
    }


}
