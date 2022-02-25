﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//using Utilities.Common;

public class HttpRESTController
{
    public static string TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI4MWRhMjI5Ni03MzkwLTRiNTQtOGY1YS04ZWQ4YWRjN2QyMGQiLCJpYXQiOjE2NDU3NjIwNjMsImV4cCI6MTY0NTg0ODQ2MywiaXNzIjoicGVwcGVyYXR0YWNrLmNvbSJ9.s92IYhKKEZOCVnuUVUXDwxCeTimRaqelsgRsMRslgx8";
    public static string REFESH_TOKEN = "";

    public static string API_URL
    {
        get
        {
            // return "http://localhost:4001/";
            return "https://api.divineclash.com/";

        }
    }
    public static bool isDebug => GameUnityData.instance.RESTDebug;
    //#if DEVELOPMENT
    //public static string API_URL = "http://localhost:5000/";
    //#else
    //	public static string API_URL = "http://34.84.194.180:5000/";
    //#endif
    //public static string API_URL
    //{
    //	get
    //	{
    //		return AppCache.Login_APIURL + "/api/";
    //	}
    //}
    public Coroutine GET(MonoBehaviour mono, string API_AddLink, string[] urlAdd, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {

        return mono.StartCoroutine(DoGET(mono, API_AddLink, urlAdd, onSuccess, onError));
    }
    public void POST(MonoBehaviour mono, string API_AddLink, bool userJSONRAW, string JSONRAW, string[] DataJSON, string[] urlAdd, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {

        mono.StartCoroutine(DoPOST(mono, API_AddLink, userJSONRAW, JSONRAW, DataJSON, urlAdd, onSuccess, onError));
    }
    public string GetURL(string API_AddLink, string[] urlAdd)
    {
        string stringAdd = null;
        if (urlAdd != null && urlAdd.Length > 0)
        {
            stringAdd = "?";
            bool _isFirst = true;
            for (int i = 0; i < urlAdd.Length; i = i + 2)
            {
                string _addValue = urlAdd[i + 1];
                if (_addValue != null && _addValue.Trim().Length > 0)
                {
                    if (urlAdd[i + 1] != null)
                    {
                        if (!_isFirst) stringAdd += "&";
                        _isFirst = false;
                        stringAdd += urlAdd[i] + "=" + urlAdd[i + 1];
                    }
                }
            }
        }
        string URL_GET = stringAdd != null ? API_URL + API_AddLink + stringAdd : API_URL + API_AddLink;
        return URL_GET;
    }
    IEnumerator DoGET(MonoBehaviour mono, string API_AddLink, string[] urlAdd, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        //  DialogWaiting.Instance.Show(true);
        string stringAdd = null;
        if (urlAdd != null && urlAdd.Length > 0)
        {
            stringAdd = "?";
            bool _isFirst = true;
            for (int i = 0; i < urlAdd.Length; i = i + 2)
            {
                string _addValue = urlAdd[i + 1];
                if (_addValue != null && _addValue.Trim().Length > 0)
                {
                    if (urlAdd[i + 1] != null)
                    {
                        if (!_isFirst) stringAdd += "&";
                        _isFirst = false;
                        stringAdd += urlAdd[i] + "=" + urlAdd[i + 1];
                    }
                }
            }
        }
        string URL_GET = stringAdd != null ? API_URL + API_AddLink + stringAdd : API_URL + API_AddLink;
        if (isDebug)
            Debug.Log("get Data from :" + URL_GET);
        try
        {
            Uri u = new Uri(URL_GET);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            onError(e.Message.ToString());
            yield break;
        }
        using (UnityWebRequest req = UnityWebRequest.Get(URL_GET))
        {
            req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
            req.SetRequestHeader("Accept", "application/json");
            req.SetRequestHeader("x-client-version", DongNHEditor.BUILD_VERSIONCODE.ToString());
            yield return req.Send();
            while (!req.isDone)
                yield return null;
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.LogError("Can't connect server.Check your connection: " + URL_GET + "_" + req.responseCode + "_" + req.error);
                onError("Can't connect server.Check your connection:");
                GameUnityData.instance.restDebugData.debugObjects.Insert(0, new RESTDebugObject()
                {
                    method = "GET",
                    url = URL_GET,
                    data = "",
                    res = req.responseCode.ToString(),
                    ShowDetail = GameUnityData.instance.restShowDetail
                });
                //   DialogWaiting.Instance.Show(false);
            }
            else
            {
                byte[] result = req.downloadHandler.data;
                string dataJSON = System.Text.Encoding.Default.GetString(result);
                //Debug.LogError(dataJSON);
                if (isDebug)
                    Debug.LogError("get data " + dataJSON);
                HttpREsultObject info = null;
                try
                {
                    info = JsonUtility.FromJson<HttpREsultObject>(dataJSON);
                    if (isDebug)
                        Debug.Log(dataJSON);
                }
                catch (Exception ex)
                {
                }
                if (info.code == RESTCode.CODE_INVALID_TOKEN)
                {
                    //if (onError != null)
                    //    onError(info.msg);
                    InvalidToken();
                    yield break;
                }
                if (info.code == RESTCode.CODE_CLIENT_OUTDATED)
                {
                    //Out version
                    // ClientOutdated(info.msg, info.data.linkUpdateAndroid, info.data.linkUpdateIOS);
                    yield break;
                }
                // else
                if (info.code != 1)
                {
                    if (onError != null)
                    {
                        //string error = Localization.Get("SERVER_" + info.code);
                        //if (error.Length > 0)
                        //    onError(Localization.Get("SERVER_" + info.code));
                        //else
                        onError(info.msg);
                        //  onError(info.msg);
                    }
                }
                else
                //if (info.code == 2)
                //{
                //    if (isDebug)
                //        Debug.LogError("Token het han");
                //    GameRESTController.Instance.APIUser_RefreshTOKEN((data) =>
                //    {
                //        mono.StartCoroutine(DoGET(mono, API_AddLink, urlAdd, onSuccess, onError));
                //    }, (err) => { });
                //    //TOKEN het han
                //}
                //else
                if (onSuccess != null)
                    onSuccess(info);

#if UNITY_EDITOR
                GameUnityData.instance.restDebugData.debugObjects.Insert(0, new RESTDebugObject()
                {
                    method = "GET",
                    url = URL_GET,
                    data = "",
                    res = dataJSON,
                    ShowDetail = GameUnityData.instance.restShowDetail
                });
                //   EditorWindow.GetWindow(typeof(RESTDebugDataEditor));
#endif
                //   DialogWaiting.Instance.Show(false);
            }
        }
    }

    IEnumerator DoPOST(MonoBehaviour mono, string API_AddLink, bool useJSONRAW, string JSONRAW, string[] DataJSON, string[] urlAdd, Action<HttpREsultObject> onSuccess, Action<string> onError)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
        //WWWForm form = new WWWForm();
        //form.AddField("username", "admin");
        //form.AddField("password", "8bYJZ58ghiG4T+czbh0hXA==");
        //    DialogWaiting.Instance.Show(true);
        byte[] postDATA = null;
        string jsonData = null;

        if (!useJSONRAW)
        {
            if (DataJSON != null)
            {

                jsonData = DataJSON.toJSON();

            }

            if (DataJSON != null)
                postDATA = System.Text.UTF8Encoding.UTF8.GetBytes(jsonData);
        }
        else
        {
            jsonData = JSONRAW;
            if (jsonData != null)
                postDATA = System.Text.UTF8Encoding.UTF8.GetBytes(jsonData);
        }


        string stringAdd = null;
        if (urlAdd != null && urlAdd.Length > 0)
        {
            stringAdd = "?";
            bool _isFirst = true;
            for (int i = 0; i < urlAdd.Length; i = i + 2)
            {
                string _addValue = urlAdd[i + 1];
                if (_addValue != null && _addValue.Trim().Length > 0)
                {
                    if (urlAdd[i + 1] != null)
                    {
                        if (!_isFirst) stringAdd += "&";
                        _isFirst = false;
                        stringAdd += urlAdd[i] + "=" + urlAdd[i + 1];
                    }
                }
            }
        }
        string URL_FINAL = stringAdd != null ? API_URL + API_AddLink + stringAdd : API_URL + API_AddLink;


        UnityWebRequest www = null;
        try
        {
            Debug.Log(URL_FINAL);
            if (jsonData != null && isDebug)
                Debug.Log("POST DATA:" + jsonData);
            www = new UnityWebRequest(URL_FINAL);

        }
        catch (Exception e)
        {
            //Debug.Log("eror ne " + e.Message));
            onError(e.Message.ToString());
            yield break;
        }
        //Debug.LogError(form.ToString());
        www.method = "POST";
        //if (Data != null)
        if (postDATA != null)
            www.uploadHandler = new UploadHandlerRaw(postDATA);
        //www.uploadHandler = new UploadHandlerRaw(form.data);
        //	Debug.LogError("Token " + TOKEN);
        if (TOKEN != null && TOKEN.Trim().Length > 0)
            www.SetRequestHeader("Authorization", "Bearer " + TOKEN);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("x-client-version", DongNHEditor.BUILD_VERSIONCODE.ToString());
        //yield return www.Send();
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            //  Debug.LogError("Can't connect server.Check your connection");
            Debug.LogError("Can't connect server.Check your connection: " + URL_FINAL + "_" + www.responseCode + "_" + www.error);
            //    Debug.Log(www.error);
            onError("Can't connect server.Check your connection");
            GameUnityData.instance.restDebugData.debugObjects.Insert(0, new RESTDebugObject()
            {
                method = "POST",
                url = URL_FINAL,
                data = jsonData,
                res = www.responseCode.ToString(),
                ShowDetail = GameUnityData.instance.restShowDetail
            });
            //    DialogWaiting.Instance.Show(false);
        }
        else
        {
            //Debug.Log("Form upload complete!");

            byte[] result = www.downloadHandler.data;
            string dataJSON = System.Text.Encoding.Default.GetString(result);
            //Debug.LogError(dataJSON);
            if (isDebug)
                Debug.Log("dataJSON from POST: " + API_AddLink + "_data:" + dataJSON);
            HttpREsultObject info = JsonUtility.FromJson<HttpREsultObject>(dataJSON);
            if (info == null)
            {
                if (onError != null)
                    onError("Not get Response");
            }
            else
            {
                if (info.code == RESTCode.CODE_INVALID_TOKEN)// || info.code == RESTCode.CODE_CLIENT_OUTDATED)
                {
                    //if (onError != null)
                    //    onError(info.msg);
                    InvalidToken();
                    yield break;
                }

                if (info.code == RESTCode.CODE_CLIENT_OUTDATED)
                {
                    //Out version
                    //   ClientOutdated(info.msg, info.data.linkUpdateAndroid, info.data.linkUpdateIOS);
                    yield break;
                }

                if (info.code != 1)
                {
                    if (onError != null)
                    {
                        onError(info.msg
                            );

                        //string error = Localization.Get("SERVER_" + info.code);
                        //if (error.Length > 0)
                        //    onError(Localization.Get("SERVER_" + info.code));
                        //else
                        //    onError(info.msg);

                    }
                }
                else
                if (info.code == -1)
                {
                    //TOKEN het han
                    if (isDebug)
                        Debug.LogError("Token het han");
                    //GameRESTController.Instance.APIUser_RefreshTOKEN((data) =>
                    //{
                    //    mono.StartCoroutine(DoPOST(mono, API_AddLink, useJSONRAW, JSONRAW, DataJSON, urlAdd, onSuccess, onError));
                    //}, (err) => { });

                }
                else
                         if (onSuccess != null)
                    onSuccess(info);

#if UNITY_EDITOR
                GameUnityData.instance.restDebugData.debugObjects.Insert(0, new RESTDebugObject()
                {
                    method = "POST",
                    url = URL_FINAL,
                    data = jsonData,
                    res = dataJSON,
                    ShowDetail = GameUnityData.instance.restShowDetail
                });
#endif

            }

            //    DialogWaiting.Instance.Show(false);
        }
    }


    //	public void GetTOKEN(MonoBehaviour mono, Action<HttpREsultObject> onSuccess, Action<string> onError)
    //	{
    //		var tokenData = new string[] { "username", "", "password", "" };
    //		mono.StartCoroutine(DoPOST(mono, "Security/Token", false, null, tokenData, null,
    // (s) =>
    // {
    //	 Debug.Log("TOKEN :" + s.Message);
    //	 HttpRESTController.TOKEN = s.Message;
    //	 if (onSuccess != null)
    //		 onSuccess(s);
    // },
    //(err) =>
    //{
    //	if (onError != null) onError(err);
    //}));
    //	}



    //[System.Serializable]
    //public class TOKENClass
    //{
    //	public string username;
    //	public string password;
    //}

    /// <summary>
    /// Dùng để Get dataJson thông thường, không phải data dạng HttpREsultObject
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="API_AddLink"></param>
    /// <param name="urlAdd"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onError"></param>
    public void GET_DataJson(MonoBehaviour mono, string API_AddLink, string[] urlAdd, Action<string> onSuccess, Action<string> onError)
    {
        mono.StartCoroutine(DoGET_DataJson(mono, API_AddLink, urlAdd, onSuccess, onError));
    }

    IEnumerator DoGET_DataJson(MonoBehaviour mono, string API_AddLink, string[] urlAdd, Action<string> onSuccess, Action<string> onError)
    {
        string stringAdd = null;
        if (urlAdd != null && urlAdd.Length > 0)
        {
            stringAdd = "?";
            bool _isFirst = true;
            for (int i = 0; i < urlAdd.Length; i = i + 2)
            {
                string _addValue = urlAdd[i + 1];
                if (_addValue != null && _addValue.Trim().Length > 0)
                {
                    if (urlAdd[i + 1] == null) continue;

                    if (!_isFirst) stringAdd += "&";
                    _isFirst = false;
                    stringAdd += urlAdd[i] + "=" + urlAdd[i + 1];
                }
            }
        }

        string URL_GET = stringAdd != null ? API_URL + API_AddLink + stringAdd : API_URL + API_AddLink;
        if (isDebug)
            Debug.Log("get Data from :" + URL_GET);
        try
        {
            Uri u = new Uri(URL_GET);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            onError(e.Message.ToString());
            yield break;
        }

        var req = UnityWebRequest.Get(URL_GET);
        req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
        req.SetRequestHeader("Accept", "application/json");
        // req.SetRequestHeader("x-client-version", DongNHEditor.BUILD_VERSIONCODE.ToString());
        yield return req.Send();
        while (!req.isDone)
            yield return null;
        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError("Can't connect server.Check your connection");
            onError?.Invoke("Can't connect server.Check your connection");
        }
        else
        {
            byte[] result = req.downloadHandler.data;
            string dataJSON = System.Text.Encoding.Default.GetString(result);
            if (isDebug)
                Debug.LogError("get data " + dataJSON);

            onSuccess?.Invoke(dataJSON);

#if UNITY_EDITOR
            GameUnityData.instance.restDebugData.debugObjects.Insert(0, new RESTDebugObject()
            {
                method = "GET",
                url = URL_GET,
                data = "",
                res = dataJSON,
                ShowDetail = GameUnityData.instance.restShowDetail
            });
#endif
        }
    }

    void InvalidToken()
    {
        //GameObject.Destroy(GameRESTController.Instance);

        //if (MainPanel.instance != null)
        //    MainPanel.instance.ShowWarningPopup("Your account has been logged in from other place", () =>
        //    {
        //        SceneLoader.LoadScene("Splash", false);
        //    }, false);
        //if (MainGamePanel.instance != null)
        //    MainGamePanel.instance.ShowWarningPopup("Your account has been logged in from other place", () =>
        //    {
        //        SceneLoader.LoadScene("Splash", false);
        //    }, false);
    }

    private void ClientOutdated(string codeMsg, string linkAndroid, string linkIOS)
    {
        //IntroPanel.Instance.ShowWaitingPanel(false);
        //if (SceneManager.GetActiveScene().name.Equals("Splash"))
        //{
        //    IntroPanel.Instance.ShowWarningPopup(codeMsg, () =>
        //    {
        //        OpenUpdateLink(linkAndroid, linkIOS);
        //    }, false);
        //}
        //else
        //{
        //    if (MainPanel.instance == null)
        //    {
        //        MainPanel.instance.ShowWarningPopup(codeMsg, () =>
        //         {
        //             OpenUpdateLink(linkAndroid, linkIOS);
        //         }, false);
        //        return;
        //    }
        //    if (MainGamePanel.instance == null)
        //    {
        //        MainGamePanel.instance.ShowWarningPopup(codeMsg, () =>
        //         {
        //             OpenUpdateLink(linkAndroid, linkIOS);
        //         }, false);
        //        return;
        //    }
        //    //  MainGamePanel.instance.ShowWarningPopup(codeMsg, false);
        //}
    }

    void OpenUpdateLink(string linkAndroid, string linkIOS)
    {
#if UNITY_ANDROID
        if (linkAndroid != null && linkAndroid.Length >= 0)
            Application.OpenURL(linkAndroid);
        else
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.beemob.idlecyber");
#else
        if (linkIOS != null && linkIOS.Length >= 0)
            Application.OpenURL(linkIOS);
        else
            Application.OpenURL("https://testflight.apple.com/join/oBfCJSCv");
#endif
        Application.Quit();
    }
}


public class RESTCode
{
    public static int CODE_INVALID_TOKEN = 97;
    public static int CODE_CLIENT_OUTDATED = 98;
}
