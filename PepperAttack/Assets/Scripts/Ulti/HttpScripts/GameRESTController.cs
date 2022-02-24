using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRESTController : MonoBehaviour
{
    public string TOKEN { get { return HttpRESTController.TOKEN; } set { HttpRESTController.TOKEN = value; } }
    public string REFESH_TOKEN { get { return HttpRESTController.REFESH_TOKEN; } set { HttpRESTController.REFESH_TOKEN = value; } }
    //public static GameRESTController Instance;

    HttpRESTController httpRESTController;

    static GameRESTController instance;
    public static GameRESTController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameRESTController>();
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public HttpImageGetterController ImageGetter;
    public GameRESTCache Cache;

    public RESTUserController UserController;

    private void Awake()
    {
        Instance = this;
#if UNITY_EDITOR
        GameUnityData.instance.restDebugData.debugObjects = new List<RESTDebugObject>();
#endif
        httpRESTController = new HttpRESTController();
        ImageGetter = new HttpImageGetterController(this);

        UserController = new RESTUserController();
        UserController.Init(httpRESTController, this);

    }

    #region Example
    //public void APICustomerLogin(string sCustID, Action<HttpREsultObject> onSuccess, Action<string> onError)
    //{
    //	httpRESTController.GET(this, "Billing/CustomerLogin", new string[] { "sCustID", sCustID }, onSuccess, onError);
    //}

    //	public void APIPostReadingBilling(string sMngObjCod, string sCardID, string sPeriod, string sYear,
    //string iReadNumPrev, string sReadStsCod, string dReadDate, string iReadNum, string sReadChange, string iAddConsum, string iOldConsum, string sXbox, string sYbox, string dReadTime, string ReadDesc, string sUserName, string TotalConsum, Action<HttpREsultObject> onSuccess, Action<string> onError)
    //	{

    //		string _jsonOne = new string[] { "MngObjCod", sMngObjCod,
    // "CardID", sCardID,"Period",sPeriod,"Year",sYear,"ReadNumPrev",iReadNumPrev,"ReadStsCod",sReadStsCod,"ReadDate",dReadDate,
    //"ReadNum",iReadNum,"ReadChange",sReadChange,"AddConsum",iAddConsum,"OldConsum",iOldConsum,"Xbox",sXbox ,"Ybox",sYbox,"ReadTime",dReadTime,"ReadDesc",ReadDesc,"UserName",sUserName,"TotalConsum",TotalConsum}.toJSON();
    //		_jsonOne = string.Format("[{0}]", _jsonOne);
    //		httpRESTController.POST(this.Mono, "Reading/PostReadingBilling", true, _jsonOne, null, null, onSuccess, onError);
    //	}

    #endregion
}
