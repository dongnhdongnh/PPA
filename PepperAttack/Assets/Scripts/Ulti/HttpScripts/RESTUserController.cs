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

    //	public void APIPostReadingBilling(string sMngObjCod, string sCardID, string sPeriod, string sYear,
    //string iReadNumPrev, string sReadStsCod, string dReadDate, string iReadNum, string sReadChange, string iAddConsum, string iOldConsum, string sXbox, string sYbox, string dReadTime, string ReadDesc, string sUserName, string TotalConsum, Action<HttpREsultObject> onSuccess, Action<string> onError)
    //	{

    //		string _jsonOne = new string[] { "MngObjCod", sMngObjCod,
    // "CardID", sCardID,"Period",sPeriod,"Year",sYear,"ReadNumPrev",iReadNumPrev,"ReadStsCod",sReadStsCod,"ReadDate",dReadDate,
    //"ReadNum",iReadNum,"ReadChange",sReadChange,"AddConsum",iAddConsum,"OldConsum",iOldConsum,"Xbox",sXbox ,"Ybox",sYbox,"ReadTime",dReadTime,"ReadDesc",ReadDesc,"UserName",sUserName,"TotalConsum",TotalConsum}.toJSON();
    //		_jsonOne = string.Format("[{0}]", _jsonOne);
    //		httpRESTController.POST(this.Mono, "Reading/PostReadingBilling", true, _jsonOne, null, null, onSuccess, onError);
    //	}

}
