using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PanelLoginController : PanelBase<PanelLoginController>
{
    [SerializeField]
    TMP_InputField if_username, if_password;

    [SerializeField]
    Button btn_login;

    // Start is called before the first frame update
    void Awake()
    {
        btn_login.onClick.AddListener(OnLoginEvent);
        if_username.onEndEdit.AddListener(OnEndEditEvent);
        if_password.onEndEdit.AddListener(OnEndEditEvent);

        if_username.text = GameSave.Cache_UserName;
        if_password.text = GameSave.Cache_Password;
    }



    private void OnEndEditEvent(string arg0)
    {
        GameSave.Cache_UserName = if_username.text;
        GameSave.Cache_Password = if_password.text;
    }

    private void OnLoginEvent()
    {
        PanelWaitingController.Instance.Init("Login");
        PanelWaitingController.Instance.Show();
        GameRESTController.Instance.UserController.Login(if_username.text, if_password.text, OnLoginDone, OnLoginError);

    }

    private void OnLoginError(string obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Warning", obj);
        PanelConfirmController.Instance.Show();
    }

    private void OnLoginDone(HttpREsultObject obj)
    {
        PanelWaitingController.Instance.Hide();
        PanelConfirmController.Instance.InitConfirm("Warning", "WELLCOME BACK", "OK", () =>
         {
             PanelLoading.LoadScene(GameConstant.GameScene.HOME);
         });
        PanelConfirmController.Instance.Show();
    }
}
